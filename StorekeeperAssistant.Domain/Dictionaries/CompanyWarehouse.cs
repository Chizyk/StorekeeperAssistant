using System.ComponentModel.DataAnnotations;
using StorekeeperAssistant.Domain.Core;

namespace StorekeeperAssistant.Domain.Dictionaries
{
    /// <summary> Склады компании </summary>
    public class CompanyWarehouse : EntityBase
    {
        /// <summary> Наименование </summary>
        [Required, StringLength(255)]
        public string Name { get; private set; }

        private CompanyWarehouse() { }

        /// <summary> Склады компании </summary>
        public CompanyWarehouse(string name)
        {
            Name = name;
        }
    }
}
