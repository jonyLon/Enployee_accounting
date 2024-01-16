using System.Collections;
using System.Text;
namespace ConsoleApp1
{

    class Employee : IComparable<Employee>
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }

        public Employee(string name, string position, int salary, string email)
        {
            Name = String.IsNullOrWhiteSpace(name) ? throw new ArgumentException(" name ") : name;
            Position = String.IsNullOrWhiteSpace(position) ? throw new ArgumentException(" position ") : position;
            Salary = salary;
            Email = String.IsNullOrWhiteSpace(email) ? throw new ArgumentException(" email ") : email;
        }
        public override string ToString()
        {
            return $"\tName: {Name}\n\tPosition: {Position}\n\tSalary: {Salary}\n\tEmail: {Email}\n\n";
        }

        public int CompareTo(Employee? other)
        {
            if (other == null) { throw new ArgumentNullException(" other "); }
            return this.Name.CompareTo(other.Name);
        }
    }

    class SalaryComparer : IComparer<Employee>
    {
        public int Compare(Employee? x, Employee? y)
        {
            if (x == null || y == null) { throw new ArgumentNullException(); }
            else return x.Salary.CompareTo(y.Salary);
        }
    }

    class Accounting
    {
        protected List<Employee> employees = new List<Employee>();

        public Accounting() { }
        public Accounting(int cap)
        {
            employees = new List<Employee>(cap);
        }
        public Accounting(params Employee[] employees) => this.employees.AddRange(employees);
        
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }        
        public void DelEmployee(Employee who)
        {
            employees.Remove(who);
        }

        public Employee SearchEmployee(string name)
        {
            Employee? who = employees.Find(x => x.Name == name);
            if (who != null)
            {
                return who;
            }
            else throw new Exception("Not found");
        }
        public Employee SearchByPosition(string pos)
        {
            Employee? who = employees.Find(x => x.Position == pos);
            if (who != null)
            {
                return who;
            }
            else throw new Exception("Not found");
        }

        public void EditEmoloyee(Employee who, Employee newone)
        {
            int index = employees.IndexOf(who);
            if (index != -1)
            {
                employees[index] = newone;
            }
            else throw new Exception("Not found");
        }
        public void SortByName()
        {
            employees.Sort();
        }
        public void SortBySalary()
        {
            employees.Sort(new SalaryComparer());
        }
        public override string ToString()
        {
            StringBuilder line = new StringBuilder("Employees dataset: \n");
            foreach (Employee employee in employees)
            {
                line.Append(employee.ToString());
            }
            return line.ToString();
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Accounting accounting = new Accounting();

            accounting.AddEmployee(new Employee("John Doe", "Software Engineer", 80000, "john.doe@example.com"));
            accounting.AddEmployee(new Employee("Jane Smith", "Project Manager", 95000, "jane.smith@example.com"));
            accounting.AddEmployee(new Employee("Mike Johnson", "Data Analyst", 75000, "mike.johnson@example.com"));
            accounting.AddEmployee(new Employee("Emily Williams", "UI/UX Designer", 85000, "emily.williams@example.com"));
            accounting.AddEmployee(new Employee("Chris Brown", "Marketing Specialist", 70000, "chris.brown@example.com"));

            Console.WriteLine(accounting.ToString());
            accounting.EditEmoloyee(accounting.SearchEmployee("John Doe"), new Employee("Alex Turner", "Quality Assurance Analyst", 78000, "alex.turner@example.com"));
            Console.WriteLine("Edit first emoloyee: \n" + accounting.ToString());
            accounting.SortByName();
            Console.WriteLine("Sort by name: \n" + accounting.ToString());
            accounting.SortBySalary();
            Console.WriteLine("Sort by salary: \n" + accounting.ToString());
            Console.WriteLine("Position search: \n" + accounting.SearchByPosition("UI/UX Designer").ToString());
        }
    }
}