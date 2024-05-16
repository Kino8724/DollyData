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
    public class Doll : INotifyPropertyChanged
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
        private int _companyId;
        public int CompanyId
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

        
        private Company _company;
        public Company Company
        {
            get => _company;
            set
            {
                if (_company != value)
                {
                    _company = value;
                    this.OnPropertyChanged();
                }
            }
        }


        public Doll(int id, string name, string description, string lineName, int amount, string image, bool isFavorite, int companyId )
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.LineName = lineName;
            this.Amount = amount;
            this.Image = image;
            this.IsFavorite = isFavorite;
            this.CompanyId = companyId;
            
        }
        public Doll(int id, string name, string description, string lineName, int amount, string image, bool isFavorite)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.LineName = lineName;
            this.Amount = amount;
            this.Image = image;
            this.IsFavorite = isFavorite;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
