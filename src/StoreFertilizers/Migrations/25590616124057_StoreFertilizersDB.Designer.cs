using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using StoreFertilizers.Models;

namespace StoreFertilizers.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("25590616124057_StoreFertilizersDB")]
    partial class StoreFertilizersDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("StoreFertilizers.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Address2");

                    b.Property<string>("City");

                    b.Property<string>("CompanyNumber");

                    b.Property<string>("ContactPerson");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("Name");

                    b.Property<string>("Phone1");

                    b.Property<string>("Phone2");

                    b.Property<string>("ZipCode");

                    b.HasKey("CustomerID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CP");

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<string>("Email");

                    b.Property<string>("EmployeeNumber");

                    b.Property<string>("Fax");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("Phone1");

                    b.Property<string>("Phone2");

                    b.HasKey("EmployeeID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bank");

                    b.Property<string>("BankBranch");

                    b.Property<string>("ChequeNumber");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("CustomerID");

                    b.Property<string>("CustomerName");

                    b.Property<string>("DeliveryByCarNumber");

                    b.Property<string>("DeliveryByPerson");

                    b.Property<DateTime?>("DeliveryDate");

                    b.Property<string>("DeliveryRefNumber");

                    b.Property<decimal?>("Discount");

                    b.Property<DateTime?>("DueDate");

                    b.Property<string>("EmployeeName");

                    b.Property<string>("InvoiceNumber");

                    b.Property<bool>("IsTax");

                    b.Property<decimal>("NetTotal");

                    b.Property<string>("Notes");

                    b.Property<bool?>("Paid");

                    b.Property<decimal?>("PaidAmount");

                    b.Property<DateTime?>("PaidCollectedDate");

                    b.Property<string>("PaidCollector");

                    b.Property<DateTime?>("PaidDate");

                    b.Property<string>("PaymentType");

                    b.Property<string>("ReceivedByPerson");

                    b.Property<DateTime?>("ReceivedProductDate");

                    b.Property<string>("ReferencePONumber");

                    b.Property<string>("ShipBy");

                    b.Property<string>("ShipTo");

                    b.Property<decimal?>("SubTotal");

                    b.Property<string>("TermOfPayment");

                    b.Property<decimal>("VAT");

                    b.HasKey("InvoiceID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.InvoiceDetails", b =>
                {
                    b.Property<int>("InvoiceDetailsID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<decimal?>("Discount");

                    b.Property<decimal?>("ExpectedProfit");

                    b.Property<int>("InvoiceID");

                    b.Property<bool?>("IsDeleted");

                    b.Property<decimal?>("PricePerUnit");

                    b.Property<int>("ProductID");

                    b.Property<int?>("PurchaseID");

                    b.Property<decimal>("Qty");

                    b.HasKey("InvoiceDetailsID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descr");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("OriginalPrice");

                    b.Property<decimal>("Price");

                    b.Property<byte[]>("ProductImage");

                    b.Property<string>("ProductNumber");

                    b.Property<int?>("ProductTypeID");

                    b.Property<int?>("UnitTypeID");

                    b.Property<int?>("UnitsPerPackage");

                    b.Property<string>("UnitsPerPackageText");

                    b.HasKey("ProductID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descr");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ProductTypeID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Provider", b =>
                {
                    b.Property<int>("ProviderID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CP");

                    b.Property<string>("City");

                    b.Property<string>("CompanyNumber");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone1");

                    b.Property<string>("Phone2");

                    b.HasKey("ProviderID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Purchase", b =>
                {
                    b.Property<int>("PurchaseID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("BillNumber");

                    b.Property<bool>("IsTax");

                    b.Property<string>("Notes");

                    b.Property<int>("ProductID");

                    b.Property<int?>("ProviderID");

                    b.Property<DateTime?>("PurchaseDate");

                    b.Property<string>("PurchaseNumber");

                    b.Property<decimal>("PurchasePricePerUnit");

                    b.Property<decimal>("Qty");

                    b.Property<decimal>("QtyRemain");

                    b.Property<decimal>("SalePrice");

                    b.Property<decimal>("VAT");

                    b.HasKey("PurchaseID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Stock", b =>
                {
                    b.Property<int>("StockID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AlertLowStock");

                    b.Property<decimal>("Balance");

                    b.Property<decimal>("FullCapStock");

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("Location");

                    b.Property<decimal>("LowCapStock");

                    b.Property<string>("Notes");

                    b.Property<int>("ProductID");

                    b.HasKey("StockID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.UnitType", b =>
                {
                    b.Property<int>("UnitTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descr");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("UnitTypeID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("Enabled");

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserID");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("StoreFertilizers.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("StoreFertilizers.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("StoreFertilizers.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Invoice", b =>
                {
                    b.HasOne("StoreFertilizers.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.InvoiceDetails", b =>
                {
                    b.HasOne("StoreFertilizers.Models.Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceID");

                    b.HasOne("StoreFertilizers.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("StoreFertilizers.Models.Purchase")
                        .WithMany()
                        .HasForeignKey("PurchaseID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Product", b =>
                {
                    b.HasOne("StoreFertilizers.Models.ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeID");

                    b.HasOne("StoreFertilizers.Models.UnitType")
                        .WithMany()
                        .HasForeignKey("UnitTypeID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Purchase", b =>
                {
                    b.HasOne("StoreFertilizers.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("StoreFertilizers.Models.Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID");
                });

            modelBuilder.Entity("StoreFertilizers.Models.Stock", b =>
                {
                    b.HasOne("StoreFertilizers.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });
        }
    }
}
