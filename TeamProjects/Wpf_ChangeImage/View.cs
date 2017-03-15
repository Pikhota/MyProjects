using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Wpf_ChangeImage
{
    public class View
    { 

     public ICommand InfoCommand
    {
        get
        {
            return new RelayCommand(x => new Window1().ShowDialog()); }
    }

    public ICommand CloseCommand
    {
        get { return new RelayCommand(CloseMethod); }
    }

    //public decimal Silver { get; private set; }
    //public decimal Experience { get; private set; }

   private void CloseMethod(object obj)
    {

    }
}
}

