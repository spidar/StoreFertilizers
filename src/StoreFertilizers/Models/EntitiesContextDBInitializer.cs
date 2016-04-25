using System.Collections.Generic;
using System;

namespace StoreFertilizers.Models {
    public class EntitiesContextInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            #region Add Users
            var users = new List<User>
            {
                new User { Name="Default user", Login="user", Password="pass", Email="hello2@user.com", Enabled=true}
            };
            foreach (User u in users)
            {
                context.CustomUsers.Add(u);
            }
            #endregion

            #region let's add some dummy customer data:
            List<Customer> customers = new List<Customer>
            {
                new Customer {Name="บริษัท เจียไต๋ จำกัด", ContactPerson="นาย เจียไต๋", Address="299-301 ถนนทรงสวัสดิ์ แขวงสัมพันธวงศ์ เขตสัมพันธวงศ์ กรุงเทพ 10100", CP="232323", CompanyNumber="3424324342", City="กรุงเทพ", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
                new Customer {Name="บริษัท พิทักษ์พืชผลเคมีเกษตร จำกัด", ContactPerson="นาย พิทักษ์", Address="37 หมู่ 8 ต.บางช้าง อ.สามพราน จ.นครปฐม 73110", CP="232323", CompanyNumber="23232323", City="นครปฐม", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"},
                new Customer {Name="บริษัท ยาร่า จำกัด", ContactPerson="นาย ยาร่า", Address="อาคารภิรัชทาวเวอร์แอ๊ดเอ็มควอเทียร์ ห้องที่ 2709-2713 ชั้นที่ 27 เลขที่ 689 ถนนสุขุมวิท แขวงคลองตันเหนือ เขตวัฒนา กรุงเทพ 10110", CP="50015", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="zaragozaactiva@hello.com"},
                new Customer {Name="บริษัท สหายเกษตรเคมีภัณฑ์", ContactPerson="นาย สหายเกษตร", Address="106 ถนนฉิมพลี แขวงฉิมพลี เขตตลิ่งชัน กรุงเทพ 10170", CP="50800", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="contacta@hello.com"},
                new Customer {Name="บริษัท ไทยเซ็นทรัลเคมี จำกัด", ContactPerson="นาย ไทย", Address=@"21/35-46 อาคารไทยวา 1 ชั้น 14-16 ถนนสาทรใต้ แขวงทุ่งมหาเมฆ เขตสาทร กรุงเทพ 10120", CP="50800", CompanyNumber="29124609", City="กรุงเทพ", Phone1="654 249068", Fax="", Email="hola@vitaminasdev.com"}
            };
            for (int i = 0; i < 5; i++)
            {
                customers.Add(new Customer()
                {
                    ContactPerson = "ชื่อผู้ติดต่อสำหรับ " + i,
                    Notes = "หมายเหตุ " + i,
                    Name = "ลูกค้า " + i,
                    Address = "ที่อยู่สำหรับลูกค้า " + i,
                    City = "กรุงเทพ",
                    CompanyNumber = "212121212" + i,
                    CP = "50800",
                    Phone1 = "2323-2222" + i,
                    Email = "email@customer" + i + ".com"
                });
            }
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            #endregion

            #region let's add some dummy Product data:
            List<Product> products = new List<Product>
            {
                new Product {Name="บริษัท เจียไต๋ จำกัด", ContactPerson="นาย เจียไต๋", Address="299-301 ถนนทรงสวัสดิ์ แขวงสัมพันธวงศ์ เขตสัมพันธวงศ์ กรุงเทพ 10100", CP="232323", CompanyNumber="3424324342", City="กรุงเทพ", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
                new Product {Name="บริษัท พิทักษ์พืชผลเคมีเกษตร จำกัด", ContactPerson="นาย พิทักษ์", Address="37 หมู่ 8 ต.บางช้าง อ.สามพราน จ.นครปฐม 73110", CP="232323", CompanyNumber="23232323", City="นครปฐม", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"},
                new Product {Name="บริษัท ยาร่า จำกัด", ContactPerson="นาย ยาร่า", Address="อาคารภิรัชทาวเวอร์แอ๊ดเอ็มควอเทียร์ ห้องที่ 2709-2713 ชั้นที่ 27 เลขที่ 689 ถนนสุขุมวิท แขวงคลองตันเหนือ เขตวัฒนา กรุงเทพ 10110", CP="50015", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="zaragozaactiva@hello.com"},
                new Product {Name="บริษัท สหายเกษตรเคมีภัณฑ์", ContactPerson="นาย สหายเกษตร", Address="106 ถนนฉิมพลี แขวงฉิมพลี เขตตลิ่งชัน กรุงเทพ 10170", CP="50800", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="contacta@hello.com"},
                new Product {Name="บริษัท ไทยเซ็นทรัลเคมี จำกัด", ContactPerson="นาย ไทย", Address=@"21/35-46 อาคารไทยวา 1 ชั้น 14-16 ถนนสาทรใต้ แขวงทุ่งมหาเมฆ เขตสาทร กรุงเทพ 10120", CP="50800", CompanyNumber="29124609", City="กรุงเทพ", Phone1="654 249068", Fax="", Email="hola@vitaminasdev.com"}
            };
            for (int i = 0; i < 5; i++)
            {
                customers.Add(new Customer()
                {
                    ContactPerson = "ชื่อผู้ติดต่อสำหรับ " + i,
                    Notes = "หมายเหตุ " + i,
                    Name = "ลูกค้า " + i,
                    Address = "ที่อยู่สำหรับลูกค้า " + i,
                    City = "กรุงเทพ",
                    CompanyNumber = "212121212" + i,
                    CP = "50800",
                    Phone1 = "2323-2222" + i,
                    Email = "email@customer" + i + ".com"
                });
            }
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            #endregion

            #region Add some dummy random invoices
            var dummy_services = new string[] { "ปุ๋ย-กระต่าย", "ปุ๋ย-ยาร่า", "ออติว่า ขนาด 500 ซีซี", "ไธอะ โนซาน ขนาด 1 กก.", "ปุ๋ย-นิโตรฟอสก้า Perfect 15-5-20", "พรีมาตรอน" };

            string invoice_number = string.Empty;
            for (int m = 1; m <= DateTime.Now.Month; m++)
            {
                for (int i = 0; i < 5; i++) {
                    Invoice invoice = new Invoice();
                    invoice.InvoiceNumber = "SH0000" + i.ToString();
                    invoice.Customer = customers[new Random(m).Next(0, customers.Count - 1)]; //random customer
                    //invoice.AdvancePaymentTax = 15;
                    invoice.Name = "Consulting services, as detailed in the invoice";
                    invoice.CreatedDate = new DateTime(DateTime.Now.Year, m, new Random().Next(1, 28)); //random date (this month)
                    invoice.DueDate = invoice.CreatedDate.AddDays(90);
                    invoice.Notes = invoice.Name + " notes";
                    invoice.Paid = new Random().Next(0, 10) >= 1; //low probability of unpaid
                    invoice.CustomerID = invoice.Customer.CustomerID;

                    int number_invoice_details = new Random().Next(4, 10);
                    for (int id = 0; id < number_invoice_details; id++)
                    {
                        invoice.InvoiceDetails.Add(new InvoiceDetails()
                        {
                            Article = dummy_services[new Random(id).Next(0, dummy_services.Length)],
                            PricePerUnit = 70,
                            Qty = new Random().Next(5, 10), //random qty
                        });
                    }
                    context.Invoices.Add(invoice);
                }
            }
            #endregion

            #region let's add a few providers
            List<Provider> providers = new List<Provider>();
            int providers_count = 80;
            var dummy_provider_names = new string[] { "Ebay", "PayPal", "Computer Store", "MarketPlace", "Hard disks from China Inc.", "Printing items Inc." };
            for (int i = 0; i < providers_count; i++)
            {
                providers.Add(new Provider()
                {
                    Name = dummy_provider_names[new Random(i).Next(0, dummy_provider_names.Length)] + " " + i,
                    Address = "Address for provider" + i,
                    City = "Zaragoza",
                    CompanyNumber = "212121212" + i,
                    CP = "50800",
                    Phone1 = "2323-2222" + i,
                    Email = "dummy_email@provider" + i + ".com"
                });
            }
            foreach (Provider p in providers)
            {
                context.Providers.Add(p);
            }
            #endregion

            #region AddExpense types
            var expense_cats = new string[] { "Automobile", "Contractors", "Marketing", "Meals", "Medical", "Misc", "Office supplies", "Rent", "Telephone/Communications", "Travel" };

            List<PurchaseType> expenseCats = new List<PurchaseType>();
            for (int ec = 0; ec < expense_cats.Length; ec++)
            {
                expenseCats.Add(new PurchaseType() { Name = expense_cats[ec], Descr = expense_cats[ec] });
            }

            foreach (PurchaseType pt in expenseCats)
            {
                context.PurchaseTypes.Add(pt);
            }
            #endregion

            #region randon Expenses
            var articles_dummy = new string[] { "Food expense", "Car expense", "Computer item", "Train ticket", "Plain ticket" };
            for (int m = 1; m < DateTime.Now.Month; m++)
            {
                int expenses_count_per_month = new Random(m).Next(5, 15);
                for (int i = 0; i < expenses_count_per_month; i++)
                {
                    context.Purchases.Add(new Purchase()
                    {
                        Provider = providers[new Random(i).Next(0, providers.Count - 1)],
                        Article = articles_dummy[new Random(i).Next(0, articles_dummy.Length - 1)],
                        Price = new Random(i).Next(10, 100),
                        VAT = 18,
                        PurchaseType = expenseCats[new Random(i).Next(0, expenseCats.Count - 1)],
                        TimeStamp = new DateTime(DateTime.Now.Year, m, new Random(i).Next(1, 28))
                    });
                }
            }
            #endregion

            // add data into context and save to db
            try
            {
                context.SaveChanges();
            }
            catch (Exception dbEx) //debug errors
            {
                 Console.Write("Seed save changes error : " + dbEx.Message);
            }
        }
    }
}