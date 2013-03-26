using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using InfiMobile.Core.Models;
using InfiMobile.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace InfiMobile.Core.ViewModels
{
    public class BaseViewModel<T> : MvxViewModel where T : Model, new()
    {
        public int _id;

        private T _model;
        public T Model 
        {
            get
            {
                if (_model == null)
                {
                    if (_id > -1)
                        _model = DatabaseService.Instance.Load<T>(_id);
                    else
                        _model = new T();
                }
                return _model;
            }

            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }

        public void Init(int id = -1)
        {
            Debug.WriteLine("ID = " + id.ToString());
            _id = id;
        }

        public ICommand CommandCancel
        {
            get
            {
                return new MvxCommand(RequestClose);
            }
        }

        protected void RequestClose()
        {
            var closer = Mvx.Resolve<IViewModelCloser>();
            closer.RequestClose(this);
        }
    }
}
