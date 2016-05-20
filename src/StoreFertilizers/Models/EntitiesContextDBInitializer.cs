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
                new Customer {Name="บ. เจียไต๋ จำกัด", ContactPerson="นาย เจียไต๋", Address="299-301 ถนนทรงสวัสดิ์ แขวงสัมพันธวงศ์ เขตสัมพันธวงศ์ กรุงเทพ 10100", Address2 = "", ZipCode="10100", CompanyNumber="3424324342", City="กรุงเทพ", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
                new Customer {Name="บ. พิทักษ์พืชผลเคมีเกษตร จำกัด", ContactPerson="นาย พิทักษ์", Address="37 หมู่ 8 ต.บางช้าง อ.สามพราน จ.นครปฐม 73110", Address2 = "", ZipCode="73110", CompanyNumber="23232323", City="นครปฐม", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"},
                new Customer {Name="บ. ยาร่า จำกัด", ContactPerson="นาย ยาร่า", Address="อาคารภิรัชทาวเวอร์แอ๊ดเอ็มควอเทียร์ ห้องที่ 2709-2713 ชั้นที่ 27", Address2 = "เลขที่ 689 ถนนสุขุมวิท แขวงคลองตันเหนือ เขตวัฒนา กรุงเทพ 10110", ZipCode="10110", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="zaragozaactiva@hello.com"},
                new Customer {Name="บ. สหายเกษตรเคมีภัณฑ์", ContactPerson="นาย สหายเกษตร", Address="106 ถนนฉิมพลี แขวงฉิมพลี เขตตลิ่งชัน กรุงเทพ 10170", Address2 = "", ZipCode="10170", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="contacta@hello.com"},
                new Customer {Name="บ. ไทยเซ็นทรัลเคมี จำกัด", ContactPerson="นาย ไทย", Address=@"21/35-46 อาคารไทยวา 1 ชั้น 14-16 ถนนสาทรใต้ แขวงทุ่งมหาเมฆ", Address2 = "เขตสาทร กรุงเทพ 10120", ZipCode="10120", CompanyNumber="29124609", City="กรุงเทพ", Phone1="654 249068", Fax="", Email="hola@vitaminasdev.com"}
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
                new Bank { Name = "กรุงเทพ", Descr = "" },
                new Bank { Name = "กรุงไทย", Descr = "" },
                new Bank { Name = "กสิกร.", Descr = "" },
                new Bank { Name = "ไทยพาณิชย์", Descr = "" },
                new Bank { Name = "กรุงศรี", Descr = "" },
                new Bank { Name = "ออมสิน", Descr = "" },
                new Bank { Name = "ธอส", Descr = "" },
                new Bank { Name = "ทหารไทย", Descr = "" }
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
                new Product { ProductNumber = "00001", Name = "กระต่าย 46-0-0 เขียว", Descr = "ปุ๋ย - กระต่าย 46-0-0 เขียว", OriginalPrice = 110, Price = 111, UnitsPerPackage = 50, UnitsPerPackageText = "กส.*50กก.", ProductType = productTypes[0], UnitType = unitTypes[0] },
                new Product { ProductNumber = "00002", Name = "กระต่าย 46-0-0 ฟ้า", Descr = "ปุ๋ย - กระต่าย 46-0-0 ฟ้า", OriginalPrice = 120, Price = 122, UnitsPerPackage = 50, UnitsPerPackageText = "กส.*50กก.", ProductType = productTypes[0], UnitType = unitTypes[0]  },
                new Product { ProductNumber = "00003", Name = "กระต่าย 46-0-0 แดง", Descr = "ปุ๋ย - กระต่าย 46-0-0 แดง", OriginalPrice = 130, Price = 133, UnitsPerPackage = 50, UnitsPerPackageText = "กส.*50กก.", ProductType = productTypes[0], UnitType = unitTypes[0]  },
                new Product { ProductNumber = "00004", Name = "ยาร่า 16-16-16", Descr = "ปุ๋ย - ยาร่า 16-16-16", OriginalPrice = 140, Price = 144, UnitsPerPackage = 50, UnitsPerPackageText = "กส.*50กก.", ProductType = productTypes[0], UnitType = unitTypes[0]  },
                new Product { ProductNumber = "00005", Name = "ยาร่า 24-7-7", Descr = "ปุ๋ย - ยาร่า 24-7-7", OriginalPrice = 150, Price = 155, UnitsPerPackage = 50, UnitsPerPackageText = "กส.*50กก.", ProductType = productTypes[0], UnitType = unitTypes[0]  },
                new Product { ProductNumber = "01001", Name = "ไดเทสเอ็ม 45 เหลือง ขนาด 1 กก.", Descr = "น้ำยา - ไดเทสเอ็ม 45 เหลือง ขนาด 1 กก.", OriginalPrice = 160, Price = 166, UnitsPerPackage = 20, UnitsPerPackageText = "ลัง*20", ProductType = productTypes[1], UnitType = unitTypes[3]  },
                new Product { ProductNumber = "01002", Name = "ไธอะโนซาน ขนาด 1 กก.", Descr = "น้ำยา - ไธอะโนซาน ขนาด 1 กก.", OriginalPrice = 170, Price = 177, UnitsPerPackage = 12, UnitsPerPackageText = "ลัง*12.", ProductType = productTypes[1], UnitType = unitTypes[4]  },
                new Product { ProductNumber = "01003", Name = "ออติวา ขนาด 500 ซีซี", Descr = "น้ำยา - ออติวา ขนาด 500 ซีซี", OriginalPrice = 180, Price = 188, UnitsPerPackage = 24, UnitsPerPackageText = "ลัง*24", ProductType = productTypes[1], UnitType = unitTypes[5]  },
                new Product { ProductNumber = "01004", Name = "OX-PREMIUM 50 KGS.", Descr = "OX-PREMIUM Packing 50 KGS.", OriginalPrice = 190, Price = 199, UnitsPerPackage = 50, UnitsPerPackageText = "กส.*50กก.", ProductType = productTypes[1], UnitType = unitTypes[3]  },
                new Product { ProductNumber = "01005", Name = "พรีมาตรอน 1 ลิตร", Descr = "พรีมาตรอน 1 ลิตร", OriginalPrice = 200, Price = 222, UnitsPerPackage = 24, UnitsPerPackageText = "ลัง*24", ProductType = productTypes[1], UnitType = unitTypes[3]  }
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
                    invoice.IsTax = new Random().Next(0, 10) >= 5;
                    invoice.InvoiceNumber = "SH0000" + i.ToString();
                    invoice.Customer = customers[new Random(m).Next(0, customers.Count - 1)]; //random customer
                    //invoice.CustomerID = invoice.Customer.CustomerID;
                    invoice.CustomerName = invoice.Customer.Name;
                    invoice.CreatedDate = new DateTime(DateTime.Now.Year, m, new Random().Next(1, 28)); //random date (this month)
                    invoice.DueDate = invoice.CreatedDate.Value.AddDays(90);
                    invoice.TermOfPayment = "เครดิต 90 วัน";
                    invoice.DeliveryDate = invoice.CreatedDate.Value.AddDays(3);
                    invoice.ReferencePONumber = "PO-0000" + i;
                    invoice.Employee = employees[new Random(m).Next(0, employees.Count - 1)]; //random employee
                    //invoice.EmployeeID = invoice.Employee.EmployeeID;
                    invoice.ShipTo = "ตามที่อยู่ลูกค้า";
                    invoice.ShipBy = "รถ";
                    invoice.DeliveryRefNumber = i.ToString();
                    invoice.DeliveryByCarNumber = "ทะเบียน รบ.82-8505 กรุงเทพมหานคร";

                    int number_invoice_details = new Random().Next(4, 10);
                    decimal subTotal = 0;
                    for (int id = 0; id < number_invoice_details; id++)
                    {
                        var invoiceDetails = new InvoiceDetails();
                        var fiftyDiscount = new Random().Next(0, 1);
                        var fiftyProfit = new Random().Next(0, 1);

                        //invoiceDetails.ProductID = products[new Random(id).Next(0, products.Count - 1)].ProductID;
                        invoiceDetails.Product = products[new Random(id).Next(0, products.Count - 1)];
                        //invoiceDetails.ProductID = invoiceDetails.Product.ProductID;
                        //invoiceDetails.UnitType = unitTypes[new Random(id).Next(0, unitTypes.Count - 1)];
                        //invoiceDetails.UnitTypeID = invoiceDetails.UnitType.UnitTypeID;
                        invoiceDetails.PricePerUnit = new Random(id).Next(100, 9999);
                        invoiceDetails.Qty = new Random(id).Next(5, 10);
                        if (fiftyDiscount == 1)
                        {
                            invoiceDetails.Discount = 10;
                        }else
                        {
                            invoiceDetails.Discount = 0;
                        }
                        if (fiftyProfit == 1)
                        {
                            invoiceDetails.ExpectedProfit = 10;
                        }else
                        {
                            invoiceDetails.ExpectedProfit = 0;
                        }
                        var amount = (invoiceDetails.PricePerUnit.Value * invoiceDetails.Qty);
                        decimal totalDiscount = 0;
                        if (invoiceDetails.Discount.Value > 0)
                        {
                            totalDiscount = amount * (invoiceDetails.Discount.Value / 100);
                        }
                        decimal totalProfit = 0;
                        if (invoiceDetails.ExpectedProfit.Value > 0)
                        {
                            totalProfit = amount * (invoiceDetails.ExpectedProfit.Value / 100);
                        }
                        invoiceDetails.Amount = amount + totalProfit - totalDiscount;
                        subTotal += invoiceDetails.Amount;

                        //invoiceDetails.IsDeleted = false;
                        invoice.InvoiceDetails.Add(invoiceDetails);
                    }

                    invoice.Paid = new Random().Next(0, 10) >= 1; //low probability of unpaid
                    if (invoice.Paid.Value)
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
                        invoice.PaidDate = invoice.CreatedDate.Value.AddDays(new Random().Next(1, 30));
                        invoice.PaidAmount = subTotal;
                        invoice.PaidCollector = "ผู้รับเงินหมายเลข " + i;
                        invoice.PaidCollectedDate = invoice.PaidDate;
                    }
                    invoice.DeliveryByPerson = "ผู้ส่งของหมายเลข " + i;
                    invoice.ReceivedByPerson = "ผู้รับสินค้าหมายเลข " + i;
                    invoice.ReceivedProductDate = invoice.DeliveryDate;
                    invoice.Notes = "หมายเหตุ : " + invoice.CustomerName;

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
            List<Provider> providers = new List<Provider>
            {
                new Provider {Name="บ. เจียไต๋ จำกัด", Address="299-301 ถนนทรงสวัสดิ์ แขวงสัมพันธวงศ์ เขตสัมพันธวงศ์ กรุงเทพ 10100", CompanyNumber="3424324342", City="กรุงเทพ", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
                new Provider {Name="บ. พิทักษ์พืชผลเคมีเกษตร จำกัด", Address="37 หมู่ 8 ต.บางช้าง อ.สามพราน จ.นครปฐม 73110", CompanyNumber="23232323", City="นครปฐม", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"},
                new Provider {Name="บ. ยาร่า จำกัด", Address="อาคารภิรัชทาวเวอร์แอ๊ดเอ็มควอเทียร์ ห้องที่ 2709-2713 ชั้นที่ 27 เลขที่ 689 ถนนสุขุมวิท แขวงคลองตันเหนือ เขตวัฒนา กรุงเทพ 10110", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="zaragozaactiva@hello.com"},
                new Provider {Name="บ. สหายเกษตรเคมีภัณฑ์", Address="106 ถนนฉิมพลี แขวงฉิมพลี เขตตลิ่งชัน กรุงเทพ 10170", CompanyNumber="BBBBBB", City="กรุงเทพ", Phone1="343-23232323", Fax="233-333333", Email="contacta@hello.com"},
                new Provider {Name="บ. ไทยเซ็นทรัลเคมี จำกัด", Address=@"21/35-46 อาคารไทยวา 1 ชั้น 14-16 ถนนสาทรใต้ แขวงทุ่งมหาเมฆ เขตสาทร กรุงเทพ 10120", CompanyNumber="29124609", City="กรุงเทพ", Phone1="654 249068", Fax="", Email="hola@vitaminasdev.com"}
            };
            
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
            //var articles_dummy = new string[] { "Food expense", "Car expense", "Computer item", "Train ticket", "Plain ticket" };
            for (int m = 1; m < DateTime.Now.Month; m++)
            {
                int expenses_count_per_month = new Random(m).Next(5, 15);
                for (int i = 0; i < expenses_count_per_month; i++)
                {
                    var qty = new Random(i).Next(5, 10);
                    var purchasePrice = new Random(i).Next(10, 100);
                    context.Purchases.Add(new Purchase()
                    {
                        PurchaseNumber = "PO000" + i,
                        PurchaseDate = new DateTime(DateTime.Now.Year, m, new Random(i).Next(1, 28)),
                        BillNumber = "0000" + i,
                        Provider = providers[new Random(i).Next(0, providers.Count - 1)],
                        ProviderName = "",
                        Qty = qty,
                        QtyRemain = qty,
                        Product = products[new Random(i).Next(0, products.Count - 1)],
                        SalePrice = new Random(i).Next(10, 100),
                        PurchasePricePerUnit = purchasePrice,
                        //UnitType = unitTypes[new Random(i).Next(0, unitTypes.Count - 1)],
                        Amount = qty * purchasePrice,
                        IsTax = new Random().Next(0, 10) >= 5,
                        //VAT = 7,
                        //PurchaseType = expenseCats[new Random(i).Next(0, expenseCats.Count - 1)],
                    });
                }
            }
            #endregion

            #region let's add some dummy Stock data:
            List<Stock> stocks = new List<Stock>
            {
                new Stock { Product = products[0], Location = "คลังสินค้า 1", Balance = 1000, FullCapStock = 1000, LowCapStock = 100, AlertLowStock = false, LastUpdated = DateTime.Now.AddMonths(-1), Notes = "หมายเหตุ 1" },
                new Stock { Product = products[1], Location = "คลังสินค้า 2", Balance = 2000, FullCapStock = 2000, LowCapStock = 200, AlertLowStock = true, LastUpdated = DateTime.Now.AddDays(-1), Notes = "หมายเหตุ 2" },
                new Stock { Product = products[2], Location = "คลังสินค้า 3", Balance = 3000, FullCapStock = 3000, LowCapStock = 300, AlertLowStock = false, LastUpdated = DateTime.Now.AddYears(-1), Notes = "หมายเหตุ 3" },
                new Stock { Product = products[3], Location = "คลังสินค้า 4", Balance = 4000, FullCapStock = 4000, LowCapStock = 400, AlertLowStock = true, LastUpdated = DateTime.Now.AddMonths(-2), Notes = "หมายเหตุ 4" },
                new Stock { Product = products[4], Location = "คลังสินค้า 5", Balance = 5000, FullCapStock = 5000, LowCapStock = 500, AlertLowStock = false, LastUpdated = DateTime.Now.AddMonths(-2), Notes = "หมายเหตุ 5" },
                new Stock { Product = products[5], Location = "คลังสินค้า 6", Balance = 6000, FullCapStock = 6000, LowCapStock = 600, AlertLowStock = true, LastUpdated = DateTime.Now.AddMonths(-2), Notes = "หมายเหตุ 6" },
                new Stock { Product = products[6], Location = "คลังสินค้า 7", Balance = 7000, FullCapStock = 7000, LowCapStock = 700, AlertLowStock = true, LastUpdated = DateTime.Now.AddMonths(-3), Notes = "หมายเหตุ 7" },
                new Stock { Product = products[7], Location = "คลังสินค้า 8", Balance = 8000, FullCapStock = 8000, LowCapStock = 800, AlertLowStock = false, LastUpdated = DateTime.Now.AddMonths(-4), Notes = "หมายเหตุ 8" },
                new Stock { Product = products[8], Location = "คลังสินค้า 9", Balance = 9000, FullCapStock = 9000, LowCapStock = 900, AlertLowStock = false, LastUpdated = DateTime.Now.AddMonths(-5), Notes = "หมายเหตุ 9" },
                new Stock { Product = products[9], Location = "คลังสินค้า 10", Balance = 10000, FullCapStock = 10000, LowCapStock = 1000, AlertLowStock = false, LastUpdated = DateTime.Now.AddMonths(-6), Notes = "หมายเหตุ 10" }
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