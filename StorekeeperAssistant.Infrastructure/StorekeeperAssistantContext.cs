using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Domain.Core;
using StorekeeperAssistant.Domain.Dictionaries;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Infrastructure
{
    /// <summary>
    /// Контекст данных приложения
    /// </summary>
    public class StorekeeperAssistantContext : DbContext, IUnitOfWork
    {
        /// <summary> Склады компании </summary>
        public DbSet<CompanyWarehouse> CompaniesWarehouses { get; set; }

        /// <summary> Номенклатуры </summary>
        public DbSet<Nomenclature> Nomenclatures { get; set; }

        /// <summary> ТМЦ - товарно-материальные ценности </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary> Перемещения товаров между складами </summary>
        public DbSet<ProductMovement>  ProductMovements { get; set; }

        /// <summary> Перемещаемые номенклатуры ТМЦ </summary>
        public DbSet<NomenclatureMovement> NomenclatureMovements { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        /// <summary>
        /// Контекст данных приложения
        /// </summary>
        public StorekeeperAssistantContext(DbContextOptions<StorekeeperAssistantContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        /// <summary>
        /// Контекст данных приложения
        /// </summary>
        public StorekeeperAssistantContext(DbContextOptions<StorekeeperAssistantContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StorekeeperAssistantContext).Assembly);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var domainEntities = this.ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Транзакция {transaction.TransactionId} не корректна");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
