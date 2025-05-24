using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMS.Facade
{
    public partial class FormFacade
    {
        public Form Form { get; set; }

        public FormFacade(Form form)
        {
            Form = form;
        }
        
    }
}
