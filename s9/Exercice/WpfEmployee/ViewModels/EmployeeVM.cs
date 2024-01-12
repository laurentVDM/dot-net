using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    public class EmployeeVM
    {
        private List<EmployeeModel> _employeesList;
        private NorthwindContext context = new NorthwindContext();

        private EmployeeModel _selectedItem;
        private List<string> _titresList;

        public List<EmployeeModel> EmployeesList
        {
            get
            {
                return _employeesList = _employeesList ?? loadEmployees();
            }
        }

        private List<EmployeeModel> loadEmployees()
        {
            List<EmployeeModel> localCollection = new List<EmployeeModel>();
            foreach (var item in context.Employees)
            {
                localCollection.Add(new EmployeeModel(item));

            }

            return localCollection;

        }

        public EmployeeModel SelectedItem
        {
            get
            {
                return _selectedItem = _selectedItem;
            }
        }

        public List<string> ListTitle
        {
            get
            {
                return _titresList = _titresList ?? loadTitle();
            }
        }

        public List<string> loadTitle()
        {
            return context.Employees.Select(x => x.TitleOfCourtesy).Distinct().ToList();
        }
    }
}
