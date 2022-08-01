namespace TestNinja.Mocking
{
    public interface IEmployeeRepository
    {
        void Delete(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        public void Delete(int id)
        {
            using (var context = new EmployeeContext())
            {
                var employee = context.Employees.Find(id);

                if (employee == null) return;

                context.Employees.Remove(employee);
                context.SaveChanges();
            }
        }
    }
}