using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DollyData.Models
{
    public class Company : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private List<Doll> _dolls;
        public List<Doll> Dolls
        {
            get => _dolls;
            set
            {
                if (_dolls != value)
                {
                    _dolls = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Company(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
