using StorekeeperAssistant.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace StorekeeperAssistant.Domain.Dictionaries
{
    /// <summary> Номенклатура </summary>
    public class Nomenclature : EntityBase
    {
        /// <summary> Наименование </summary>
        [Required, StringLength(255)]
        public string Name { get; private set; }

        private Nomenclature() { }

        /// <summary> Номенклатура </summary>
        public Nomenclature(string name)
        {
            Name = name;
        }
    }
}
