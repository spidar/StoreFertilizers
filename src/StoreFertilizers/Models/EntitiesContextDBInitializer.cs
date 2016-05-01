﻿using System.Collections.Generic;
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
                new Customer {Name="บริษัท เจียไต๋ จำกัด", ContactPerson="นาย เจียไต๋", Address="299-301 ถนนทรงสวัสดิ์ แขวงสัมพันธวงศ์ เขตสัมพันธวงศ์ กรุงเทพ 10100", Address2 = "", ZipCode="10100", CompanyNumber="3424324342", City="กรุงเทพ", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
                new Customer {Name="บริษัท พิทักษ์พืชผลเคมีเกษตร จำกัด", ContactPerson="นาย พิทักษ์", Address="37 หมู่ 8 ต.บางช้าง อ.สามพราน จ.นครปฐม 73110", Address2 = "", ZipCode="73110", CompanyNumber="23232323", City="นครปฐม", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"},
                new Customer {Name="บริษัท ยาร่า จำกัด", ContactPerson="นาย ยาร่า", Address="อาคารภิรัชทาวเวอร์แอ๊ดเอ็มควอเทียร์ ห้องที่ 2709-2713 ชั้นที่ 27", Address2 = "เลขที่ 689 ถนนสุขุมวิท แขวงคลองตันเหนือ เขตวัฒนา กรุงเทพ 10110", ZipCode="10110", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="zaragozaactiva@hello.com"},
                new Customer {Name="บริษัท สหายเกษตรเคมีภัณฑ์", ContactPerson="นาย สหายเกษตร", Address="106 ถนนฉิมพลี แขวงฉิมพลี เขตตลิ่งชัน กรุงเทพ 10170", Address2 = "", ZipCode="10170", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="contacta@hello.com"},
                new Customer {Name="บริษัท ไทยเซ็นทรัลเคมี จำกัด", ContactPerson="นาย ไทย", Address=@"21/35-46 อาคารไทยวา 1 ชั้น 14-16 ถนนสาทรใต้ แขวงทุ่งมหาเมฆ", Address2 = "เขตสาทร กรุงเทพ 10120", ZipCode="10120", CompanyNumber="29124609", City="กรุงเทพ", Phone1="654 249068", Fax="", Email="hola@vitaminasdev.com"}
            };
            for (int i = 0; i < 5; i++)
            {
                customers.Add(new Customer()
                {
                    ContactPerson = "ชื่อผู้ติดต่อสำหรับ " + i,
                    Notes = "หมายเหตุ " + i,
                    Name = "ลูกค้า " + i,
                    Address = "ที่อยู่สำหรับลูกค้า " + i,
                    Address2 = "" + i,
                    City = "กรุงเทพ",
                    CompanyNumber = "212121212" + i,
                    ZipCode = "50800",
                    Phone1 = "2323-2222" + i,
                    Email = "email@customer" + i + ".com"
                });
            }
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            #endregion            
            
            #region let's add some dummy ProductType data:
            List<ProductType> productTypes = new List<ProductType>
            {
                new ProductType { Name = "ปุ๋ย", Descr = "" },
                new ProductType { Name = "ยา", Descr = "" }
            };
            foreach (ProductType item in productTypes)
            {
                context.ProductTypes.Add(item);
            }
            #endregion

            #region let's add some dummy UnitType data:
            List<UnitType> unitTypes = new List<UnitType>
            {
                new UnitType { Name = "กระสอบ", Descr = "" },
                new UnitType { Name = "ตัน", Descr = "" },
                new UnitType { Name = "กิโลกรัม.", Descr = "" },
                new UnitType { Name = "ถุง", Descr = "" },
                new UnitType { Name = "กระปุก", Descr = "" },
                new UnitType { Name = "ขวด", Descr = "" }
            };
            foreach (UnitType item in unitTypes)
            {
                context.UnitTypes.Add(item);
            }
            #endregion

            #region let's add some dummy Bank data:
            List<Bank> banks = new List<Bank>
            {
                new Bank { Name = "ธ.กรุงเทพ", Descr = "" },
                new Bank { Name = "ธ.กรุงไทย", Descr = "" },
                new Bank { Name = "ธ.กสิกร.", Descr = "" },
                new Bank { Name = "ธ.ไทยพาณิชย์.", Descr = "" },
                new Bank { Name = "ธ.กรุงศรี", Descr = "" },
                new Bank { Name = "ธ.ออมสิน", Descr = "" },
                new Bank { Name = "ธ.ธอส", Descr = "" },
                new Bank { Name = "ธ.ทหารไทย", Descr = "" }
            };
            foreach (Bank item in banks)
            {
                context.Banks.Add(item);
            }
            #endregion

            #region let's add some dummy PaymentType data:
            List<PaymentType> paymentTypes = new List<PaymentType>
            {
                new PaymentType { Name = "เงินสด", Descr = "" },
                new PaymentType { Name = "เช็ค", Descr = "" },
                new PaymentType { Name = "โอนเงิน", Descr = "" },
            };
            foreach(PaymentType pt in paymentTypes)
            {
                context.PaymentTypes.Add(pt);
            }
            #endregion

            #region let's add some dummy employee data:
            List<Employee> employees = new List<Employee>
            {
                new Employee {EmployeeNumber = "8001", Name="นาย กิจ อยู่เย็น", ContactPerson="นาง มี อยู่เย็น", Address="299-301 ถนนทรงสวัสดิ์ แขวงสัมพันธวงศ์ เขตสัมพันธวงศ์ กรุงเทพ 10100", CP="232323", City="กรุงเทพ", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
                new Employee {EmployeeNumber = "8002", Name="นาย ขิง แซ่ตั้ง", ContactPerson="นาง วา แช่ตั้ง", Address="37 หมู่ 8 ต.บางช้าง อ.สามพราน จ.นครปฐม 73110", CP="232323", City="นครปฐม", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"},
                new Employee {EmployeeNumber = "8003", Name="นาย วัง ทองน้อย", ContactPerson="นาง จิน ทองน้อย", Address="อาคารภิรัชทาวเวอร์แอ๊ดเอ็มควอเทียร์ ห้องที่ 2709-2713 ชั้นที่ 27 เลขที่ 689 ถนนสุขุมวิท แขวงคลองตันเหนือ เขตวัฒนา กรุงเทพ 10110", CP="50015", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="zaragozaactiva@hello.com"},
                new Employee {EmployeeNumber = "8004", Name="นาย ขุน เกรียงไกร", ContactPerson="นาง ดี เกรียงไกร", Address="106 ถนนฉิมพลี แขวงฉิมพลี เขตตลิ่งชัน กรุงเทพ 10170", CP="50800", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="contacta@hello.com"},
                new Employee {EmployeeNumber = "8005", Name="นาย ปิง บ้านดี", ContactPerson="นาง ไฝ๋ บ้านดี", Address=@"21/35-46 อาคารไทยวา 1 ชั้น 14-16 ถนนสาทรใต้ แขวงทุ่งมหาเมฆ เขตสาทร กรุงเทพ 10120", CP="50800", City="กรุงเทพ", Phone1="654 249068", Fax="", Email="hola@vitaminasdev.com"}
            };
            foreach (Employee c in employees)
            {
                context.Employees.Add(c);
            }
            #endregion

            #region let's add some dummy Product data:
            List<Product> products = new List<Product>
            {
                new Product { ProductNumber = "00001", Name = "กระต่าย 46-0-0 เขียว", Descr = "ปุ๋ย - กระต่าย 46-0-0 เขียว", OriginalPrice = 110, Price = 111, ProductType = productTypes[0], UnitTypeID = unitTypes[0].UnitTypeID },
                new Product { ProductNumber = "00002", Name = "กระต่าย 46-0-0 ฟ้า", Descr = "ปุ๋ย - กระต่าย 46-0-0 ฟ้า", OriginalPrice = 120, Price = 122, ProductType = productTypes[0], UnitTypeID = unitTypes[0].UnitTypeID  },
                new Product { ProductNumber = "00003", Name = "กระต่าย 46-0-0 แดง", Descr = "ปุ๋ย - กระต่าย 46-0-0 แดง", OriginalPrice = 130, Price = 133, ProductType = productTypes[0], UnitTypeID = unitTypes[0].UnitTypeID  },
                new Product { ProductNumber = "00004", Name = "ยาร่า 16-16-16", Descr = "ปุ๋ย - ยาร่า 16-16-16", OriginalPrice = 140, Price = 144, ProductType = productTypes[0], UnitTypeID = unitTypes[0].UnitTypeID  },
                new Product { ProductNumber = "00005", Name = "ยาร่า 24-7-7", Descr = "ปุ๋ย - ยาร่า 24-7-7", OriginalPrice = 150, Price = 155, ProductType = productTypes[0], UnitTypeID = unitTypes[0].UnitTypeID  },
                new Product { ProductNumber = "01001", Name = "ไดเทสเอ็ม 45 เหลือง ขนาด 1 กก.", Descr = "น้ำยา - ไดเทสเอ็ม 45 เหลือง ขนาด 1 กก.", OriginalPrice = 160, Price = 166, ProductType = productTypes[1], UnitTypeID = unitTypes[3].UnitTypeID  },
                new Product { ProductNumber = "01002", Name = "ไธอะโนซาน ขนาด 1 กก.", Descr = "น้ำยา - ไธอะโนซาน ขนาด 1 กก.", OriginalPrice = 170, Price = 177, ProductType = productTypes[1], UnitTypeID = unitTypes[4].UnitTypeID  },
                new Product { ProductNumber = "01003", Name = "ออติวา ขนาด 500 ซีซี", Descr = "น้ำยา - ออติวา ขนาด 500 ซีซี", OriginalPrice = 180, Price = 188, ProductType = productTypes[1], UnitTypeID = unitTypes[5].UnitTypeID  },
                new Product { ProductNumber = "01004", Name = "OX-PREMIUM 50 KGS.", Descr = "OX-PREMIUM Packing 50 KGS.", OriginalPrice = 190, Price = 199, ProductType = productTypes[1], UnitTypeID = unitTypes[3].UnitTypeID  },
                new Product { ProductNumber = "01005", Name = "พรีมาตรอน 1 ลิตร", Descr = "พรีมาตรอน 1 ลิตร", OriginalPrice = 200, Price = 222, ProductType = productTypes[1], UnitTypeID = unitTypes[3].UnitTypeID  }
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            #endregion

            #region Add some dummy random invoices
            //var dummy_services = new string[] { "ปุ๋ย-กระต่าย", "ปุ๋ย-ยาร่า", "ออติว่า ขนาด 500 ซีซี", "ไธอะ โนซาน ขนาด 1 กก.", "ปุ๋ย-นิโตรฟอสก้า Perfect 15-5-20", "พรีมาตรอน" };

            string invoice_number = string.Empty;
            for (int m = 1; m <= DateTime.Now.Month; m++)
            {
                for (int i = 0; i < 5; i++) {
                    Invoice invoice = new Invoice();
                    invoice.InvoiceNumber = "SH0000" + i.ToString();
                    invoice.Customer = customers[new Random(m).Next(0, customers.Count - 1)]; //random customer
                    //invoice.CustomerID = invoice.Customer.CustomerID;
                    invoice.Name = @"ใบส่งสินค้า / ใบแจ้งหนี้";
                    invoice.CreatedDate = new DateTime(DateTime.Now.Year, m, new Random().Next(1, 28)); //random date (this month)
                    invoice.DueDate = invoice.CreatedDate.AddDays(90);
                    invoice.TermOfPayment = "เครดิต 90 วัน";
                    invoice.DeliveryDate = invoice.CreatedDate.AddDays(3);
                    invoice.ReferencePONumber = "PO-0000" + i;
                    invoice.Employee = employees[new Random(m).Next(0, employees.Count - 1)]; //random employee
                    //invoice.EmployeeID = invoice.Employee.EmployeeID;
                    invoice.ShipTo = "ตามที่อยู่ลูกค้า";
                    invoice.ShipBy = "รถ";
                    invoice.DeliveryByCarNumber = "ทะเบียน รบ.82-8505 กรุงเทพมหานคร";

                    int number_invoice_details = new Random().Next(4, 10);
                    decimal subTotal = 0;
                    for (int id = 0; id < number_invoice_details; id++)
                    {
                        var invoiceDetails = new InvoiceDetails();
                        var fiftyDiscount = new Random().Next(0, 10) >= 5;
                        var fiftyProfit = new Random().Next(0, 10) >= 5;

                        invoiceDetails.Product = products[new Random(id).Next(0, products.Count - 1)];
                        //invoiceDetails.ProductID = invoiceDetails.Product.ProductID;
                        invoiceDetails.UnitTypeID = unitTypes[new Random().Next(0, unitTypes.Count - 1)].UnitTypeID;
                        //invoiceDetails.UnitTypeID = invoiceDetails.UnitType.UnitTypeID;
                        invoiceDetails.PricePerUnit = new Random().Next(100, 999999);
                        invoiceDetails.Qty = new Random().Next(5, 10);
                        if (fiftyDiscount)
                        {
                            invoiceDetails.Discount = 10;
                        }
                        if (fiftyProfit)
                        {
                            invoiceDetails.ExpectedProfit = 20;
                        }
                        var amount = (invoiceDetails.PricePerUnit * invoiceDetails.Qty);
                        var totalDiscount = amount * (invoiceDetails.Discount / 100);
                        var totalProfit = amount * (invoiceDetails.ExpectedProfit / 100);
                        invoiceDetails.Amount = amount + totalProfit - totalDiscount;
                        subTotal += invoiceDetails.Amount;

                        invoice.InvoiceDetails.Add(invoiceDetails);
                    }

                    invoice.Paid = new Random().Next(0, 10) >= 1; //low probability of unpaid
                    if (invoice.Paid)
                    {
                        invoice.PaymentType = paymentTypes[new Random(m).Next(0, paymentTypes.Count - 1)]; //random payment type
                        //invoice.PaymentTypeID = invoice.PaymentType.PaymentTypeID;
                        if (!invoice.PaymentType.Name.Equals("เงินสด"))
                        {
                            invoice.Bank = banks[new Random(m).Next(0, banks.Count - 1)]; //random bank
                            //invoice.BankID = invoice.Bank.BankID;
                            invoice.BankBranch = "สาขา " + i;
                            if (invoice.PaymentType.Name.Equals("เช็ค"))
                            {
                                invoice.ChequeNumber = "CHEQUE-0000" + i;
                            }
                        }
                        invoice.PaidDate = invoice.CreatedDate.AddDays(new Random().Next(1, 30));
                        invoice.PaidAmount = subTotal;
                        invoice.PaidCollector = "ผู้รับเงินหมายเลข " + i;
                        invoice.PaidCollectedDate = invoice.PaidDate;
                    }
                    invoice.DeliveryByPerson = "ผู้ส่งของหมายเลข " + i;
                    invoice.ReceivedByPerson = "ผู้รับสินค้าหมายเลข " + i;
                    invoice.ReceivedProductDate = invoice.DeliveryDate;
                    invoice.Notes = "หมายเหตุ : " + invoice.Name;

                    var fiftyVAT = new Random().Next(0, 10) >= 5;

                    invoice.SubTotal = subTotal;
                    invoice.Discount = 0;
                    if (fiftyVAT)
                    {
                        invoice.VAT = 7;
                    }
                    invoice.NetTotal = subTotal * (1 + invoice.VAT / 100);
                    
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

            #region let's add some dummy Stock data:
            List<Stock> stocks = new List<Stock>
            {
                new Stock { Product = products[0], Location = "คลังสินค้า 1", Amount = 1000, LastUpdated = DateTime.Now.AddMonths(-1), Notes = "หมายเหตุ 1" },
                new Stock { Product = products[1], Location = "คลังสินค้า 2", Amount = 2000, LastUpdated = DateTime.Now.AddDays(-1), Notes = "หมายเหตุ 2" },
                new Stock { Product = products[2], Location = "คลังสินค้า 3", Amount = 3000, LastUpdated = DateTime.Now.AddYears(-1), Notes = "หมายเหตุ 3" },
                new Stock { Product = products[3], Location = "คลังสินค้า 4", Amount = 4000, LastUpdated = DateTime.Now.AddMonths(-2), Notes = "หมายเหตุ 4" },
                new Stock { Product = products[4], Location = "คลังสินค้า 5", Amount = 5000, LastUpdated = DateTime.Now.AddMonths(-2), Notes = "หมายเหตุ 5" },
                new Stock { Product = products[5], Location = "คลังสินค้า 6", Amount = 6000, LastUpdated = DateTime.Now.AddMonths(-2), Notes = "หมายเหตุ 6" },
                new Stock { Product = products[6], Location = "คลังสินค้า 7", Amount = 7000, LastUpdated = DateTime.Now.AddMonths(-3), Notes = "หมายเหตุ 7" },
                new Stock { Product = products[7], Location = "คลังสินค้า 8", Amount = 8000, LastUpdated = DateTime.Now.AddMonths(-4), Notes = "หมายเหตุ 8" },
                new Stock { Product = products[8], Location = "คลังสินค้า 9", Amount = 9000, LastUpdated = DateTime.Now.AddMonths(-5), Notes = "หมายเหตุ 9" },
                new Stock { Product = products[9], Location = "คลังสินค้า 10", Amount = 10000, LastUpdated = DateTime.Now.AddMonths(-6), Notes = "หมายเหตุ 10" }
            };
            foreach (Stock p in stocks)
            {
                context.Stock.Add(p);
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