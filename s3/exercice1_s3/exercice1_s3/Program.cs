using exercice1_s3.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

//Ex1
using (NorthwindContext context = new NorthwindContext())
{
    Console.WriteLine("Entrez une ville");
    string city = Console.ReadLine();

    IQueryable<Customer> customers = from Customer c in context.Customers
                                      where c.City == city
                                      select c;
    foreach (Customer c in customers)
    {
        Console.WriteLine(c.ContactName);
    }
    Console.WriteLine("");
}
//Ex2
    using (NorthwindContext context = new NorthwindContext())
{
    IQueryable<Category> categories = from Category c in context.Categories
                                     where(c.CategoryName == "Beverages"
                                     || c.CategoryName == "Condiments")
                                     select c;

    foreach (Category c in categories)
    {
        Console.WriteLine("category : " + c.CategoryName);

        foreach (Product p in c.Products)
        {
            Console.WriteLine("product : " + p.ProductName);
        }
    }
    Console.WriteLine("");
}

//Ex3
    using (NorthwindContext context = new NorthwindContext())
{
    IQueryable<Category> categories = from Category c in context.Categories
                                      .Include("Products")
                                      where (c.CategoryName == "Beverages"
                                      || c.CategoryName == "Condiments")
                                      select c;

    foreach (Category c in categories)
    {
        Console.WriteLine("category : " + c.CategoryName);

        foreach (Product p in c.Products)
        {
            Console.WriteLine("product : " + p.ProductName);
        }
    }
    Console.WriteLine("");
}

//Ex4
using (NorthwindContext context = new NorthwindContext())
{
    Console.WriteLine("Entrez l'ID d'un client");
    string id_client = Console.ReadLine();

    IQueryable<Order> orders = from Order o in context.Orders
                               where (o.OrderDate != null
                               && o.CustomerId == id_client
                               && o.ShippedDate != null)
                               orderby o.OrderDate descending
                               select o;
    foreach (Order o in orders)
    {
        Console.WriteLine("CustomerId : " + o.CustomerId
            + " OrderDate : " + o.OrderDate
            + " Shipped date : " + o.ShippedDate);

    }
        Console.WriteLine("");
}

//Ex5
using (NorthwindContext context = new NorthwindContext())
{
    IQueryable<OrderDetail> orderDetails = from OrderDetail o in context.OrderDetails
                                        group o by o.ProductId into ProductGroup
                                        orderby ProductGroup.Key
                                           select new OrderDetail
                                        {
                                            ProductId = ProductGroup.Key,
                                            UnitPrice = ProductGroup.Sum(o=>o.UnitPrice*o.Quantity)
                                        };
    foreach (OrderDetail o in orderDetails)
    {
        Console.WriteLine(o.ProductId + " ----> " + o.UnitPrice);      
    }
    Console.WriteLine("");
}


//Ex6-chatgpt
using (NorthwindContext context = new NorthwindContext())
{
    Console.WriteLine("Liste des employés de la région Western");

    // Sélectionnez les territoires de la région "Western"
    IQueryable<Territory> territories = from Territory t in context.Territories
                                        where t.Region.RegionDescription == "Western"
                                        select t;

    // Ensuite, sélectionnez les employés associés à ces territoires
    var employeesInWesternRegion = from Employee e in context.Employees
                                   where territories.Any(t => e.Territories.Contains(t))
                                   select e;

    foreach (Employee employee in employeesInWesternRegion)
    {
        Console.WriteLine(employee.FirstName + " " + employee.LastName);
    }
    Console.WriteLine("");
}

//Ex7
using (NorthwindContext context = new NorthwindContext())
{
    
    Console.WriteLine("");
}