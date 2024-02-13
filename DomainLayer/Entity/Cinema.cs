using MoviesProject.DomainLayer.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace MoviesProject.DomainLayer.Entity
{
    public class Cinema :IEntity
    {
        public int ID { get; set; }
        public string Name { get; private set; }

        public string Address { get; private set; }
        
        public Cinema() { }

        [SetsRequiredMembers]
        public Cinema(string _name, string _address)
        {
            Name = _name;
            Address = _address;
        }
    }
   
}
