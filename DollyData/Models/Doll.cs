using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DollyData.Models
{
    public class Doll : DbObject, INotifyPropertyChanged
    {
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
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _lineName;
        public string LineName
        {
            get => _lineName;
            set
            {
                if (_lineName != value)
                {
                    _lineName = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Guid _companyId;
        public Guid CompanyId
        {
            get => _companyId;
            set
            {
                if (_companyId != value)
                {
                    _companyId = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Doll(string name, string description, string lineName, int amount, string image, bool isFavorite, Guid company )
        {
            this.Name = name;
            this.Description = description;
            this.LineName = lineName;
            this.Amount = amount;
            this.Image = image;
            this.IsFavorite = isFavorite;
            this.CompanyId = company;
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
