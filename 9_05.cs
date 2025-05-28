using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public List<string> Projects { get; set; } = new List<string>();
}

class Program
{
    static void Main()
    {
        //1
        string xmlFilePath = "employees.xml";
        XDocument xmlDoc;

        if (!File.Exists(xmlFilePath))
        {
            xmlDoc = new XDocument(
                new XElement("Employees",
                    new XElement("Employee", new XAttribute("Id", 1),
                        new XElement("FirstName", "Alice"),
                        new XElement("LastName", "Smith"),
                        new XElement("Department", "HR"),
                        new XElement("Position", "Manager"),
                        new XElement("Salary", 5000),
                        new XElement("Projects",
                            new XElement("Project", "Recruitment"),
                            new XElement("Project", "Training")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 2),
                        new XElement("FirstName", "Bob"),
                        new XElement("LastName", "Johnson"),
                        new XElement("Department", "IT"),
                        new XElement("Position", "Developer"),
                        new XElement("Salary", 4000),
                        new XElement("Projects",
                            new XElement("Project", "App Development"),
                            new XElement("Project", "Testing")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 3),
                        new XElement("FirstName", "Charlie"),
                        new XElement("LastName", "Brown"),
                        new XElement("Department", "IT"),
                        new XElement("Position", "Senior Developer"),
                        new XElement("Salary", 6000),
                        new XElement("Projects",
                            new XElement("Project", "App Development"),
                            new XElement("Project", "Cloud Migration"),
                            new XElement("Project", "Security Audit")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 4),
                        new XElement("FirstName", "Diana"),
                        new XElement("LastName", "Prince"),
                        new XElement("Department", "Marketing"),
                        new XElement("Position", "Content Writer"),
                        new XElement("Salary", 3500),
                        new XElement("Projects",
                            new XElement("Project", "Social Media Campaign"),
                            new XElement("Project", "SEO Optimization")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 5),
                        new XElement("FirstName", "Edward"),
                        new XElement("LastName", "Norton"),
                        new XElement("Department", "Finance"),
                        new XElement("Position", "Accountant"),
                        new XElement("Salary", 4800),
                        new XElement("Projects",
                            new XElement("Project", "Budget Planning"),
                            new XElement("Project", "Tax Reporting")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 6),
                        new XElement("FirstName", "Fiona"),
                        new XElement("LastName", "Gallagher"),
                        new XElement("Department", "IT"),
                        new XElement("Position", "QA Engineer"),
                        new XElement("Salary", 4200),
                        new XElement("Projects",
                            new XElement("Project", "Testing"),
                            new XElement("Project", "Automation")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 7),
                        new XElement("FirstName", "George"),
                        new XElement("LastName", "Costanza"),
                        new XElement("Department", "Sales"),
                        new XElement("Position", "Sales Manager"),
                        new XElement("Salary", 5500),
                        new XElement("Projects",
                            new XElement("Project", "Client Acquisition"),
                            new XElement("Project", "Product Launch")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 8),
                        new XElement("FirstName", "Hannah"),
                        new XElement("LastName", "Baker"),
                        new XElement("Department", "IT"),
                        new XElement("Position", "DevOps Engineer"),
                        new XElement("Salary", 5800),
                        new XElement("Projects",
                            new XElement("Project", "CI/CD Pipeline"),
                            new XElement("Project", "Monitoring Tools")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 9),
                        new XElement("FirstName", "Ian"),
                        new XElement("LastName", "Malcolm"),
                        new XElement("Department", "RnD"),
                        new XElement("Position", "Research Scientist"),
                        new XElement("Salary", 7000),
                        new XElement("Projects",
                            new XElement("Project", "AI Research"),
                            new XElement("Project", "Data Analysis")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 10),
                        new XElement("FirstName", "Jessica"),
                        new XElement("LastName", "Jones"),
                        new XElement("Department", "Legal"),
                        new XElement("Position", "Lawyer"),
                        new XElement("Salary", 6500),
                        new XElement("Projects",
                            new XElement("Project", "Compliance Check"),
                            new XElement("Project", "Contract Review")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 11),
                        new XElement("FirstName", "Kevin"),
                        new XElement("LastName", "Spacey"),
                        new XElement("Department", "Marketing"),
                        new XElement("Position", "Marketing Manager"),
                        new XElement("Salary", 5200),
                        new XElement("Projects",
                            new XElement("Project", "Brand Awareness"),
                            new XElement("Project", "Event Management"),
                            new XElement("Project", "Market Research")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 12),
                        new XElement("FirstName", "Lisa"),
                        new XElement("LastName", "Simpson"),
                        new XElement("Department", "IT"),
                        new XElement("Position", "Junior Developer"),
                        new XElement("Salary", 3000),
                        new XElement("Projects",
                            new XElement("Project", "Bug Fixing")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 13),
                        new XElement("FirstName", "Maggie"),
                        new XElement("LastName", "Greene"),
                        new XElement("Department", "HR"),
                        new XElement("Position", "Recruiter"),
                        new XElement("Salary", 3800),
                        new XElement("Projects",
                            new XElement("Project", "Talent Acquisition")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 14),
                        new XElement("FirstName", "Nathan"),
                        new XElement("LastName", "Drake"),
                        new XElement("Department", "Sales"),
                        new XElement("Position", "Sales Executive"),
                        new XElement("Salary", 4700),
                        new XElement("Projects",
                            new XElement("Project", "Lead Generation"),
                            new XElement("Project", "Customer Retention")
                        )
                    ),
                    new XElement("Employee", new XAttribute("Id", 15),
                        new XElement("FirstName", "Oscar"),
                        new XElement("LastName", "Wilde"),
                        new XElement("Department", "Finance"),
                        new XElement("Position", "Financial Analyst"),
                        new XElement("Salary", 5100),
                        new XElement("Projects",
                            new XElement("Project", "Financial Forecasting"),
                            new XElement("Project", "Investment Planning")
                        )
                    )
                )
            );
            xmlDoc.Save(xmlFilePath);
        }
        else
        {
            xmlDoc = XDocument.Load(xmlFilePath);
        }

        //2
        Console.WriteLine("\nIT Department Employees:");
        var itEmployees = xmlDoc.Descendants("Employee")
            .Where(e => e.Element("Department").Value.Equals("IT"));
        foreach (var e in itEmployees)
        {
            Console.WriteLine($"{e.Element("FirstName").Value} {e.Element("LastName").Value} - " +
                $"{e.Element("Position").Value} ({e.Element("Department").Value})");
        }

        //3
        Console.WriteLine("\nEmployees with multiple projects:");
        var multiProjectEmployees = xmlDoc.Descendants("Employee")
            .Where(e => e.Element("Projects").Elements("Project").Count() > 1);
        foreach (var e in multiProjectEmployees)
        {
            Console.WriteLine($"{e.Element("FirstName").Value} {e.Element("LastName").Value} - " +
                $"Projects: {string.Join(", ", e.Element("Projects").Elements("Project").Select(p => p.Value))}");
        }

        // 4
        var newEmployee = new XElement("Employee", new XAttribute("Id", 3),
            new XElement("FirstName", "Charlie"),
            new XElement("LastName", "Brown"),
            new XElement("Department", "IT"),
            new XElement("Position", "QA Engineer"),
            new XElement("Salary", 3500),
            new XElement("Projects",
                new XElement("Project", "Testing"),
                new XElement("Project", "Documentation")));
        xmlDoc.Root.Add(newEmployee);

        var employeeToUpdate = xmlDoc.Descendants("Employee")
            .FirstOrDefault(e => (int)e.Attribute("Id") == 1);
        employeeToUpdate.Element("Position").Value = "Senior Manager";
        
        var employeeToRemove = xmlDoc.Descendants("Employee")
            .FirstOrDefault(e => (int)e.Attribute("Id") == 2);
        employeeToRemove.Remove();
       
        xmlDoc.Save(xmlFilePath);

        //5
        var employees = xmlDoc.Descendants("Employee")
            .Select(e => new Employee
            {
                Id = (int)e.Attribute("Id"),
                FirstName = e.Element("FirstName").Value,
                LastName = e.Element("LastName").Value,
                Department = e.Element("Department").Value,
                Position = e.Element("Position").Value,
                Salary = decimal.Parse(e.Element("Salary").Value),
                Projects = e.Element("Projects").Elements("Project").Select(p => p.Value).ToList()
            }).ToList();

        foreach (var e in employees)
        {
            Console.WriteLine($"{e.Id}: {e.FirstName} {e.LastName}, " +
                $"{e.Position} in {e.Department}, " +
                $"Salary: {e.Salary}, Projects: {string.Join(", ", e.Projects)}");
        }
    }
}
