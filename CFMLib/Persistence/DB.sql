CREATE DATABASE CoffeeShop;
use CoffeeShop;
-- drop database coffeeshop;

-- CREATE DATABASE AND TABLES
create table Staffs(
Staff_ID int NOT NULL auto_increment,
Staff_Name varchar(50) NOT NULL,
User_Name varchar(50) NOT NULL,
password varchar(50) NOT NULL,
Staff_Status int NOT NULL,
PRIMARY KEY (Staff_ID)
);

create table Orders(
Order_ID int NOT NULL auto_increment unique,
Order_Staff_ID int NOT NULL,
Order_Date datetime NOT NULL default current_timestamp(),
Order_Status int NOT NULL,
PRIMARY KEY (Order_ID),
FOREIGN KEY (Order_Staff_ID) REFERENCES Staffs(Staff_ID)
);

create table Products(
Product_ID int NOT NULL auto_increment,
Product_Name varchar(50) NOT NULL,
Descriptions varchar(50),
PRIMARY KEY (Product_ID)
);

create table Sizes(
Size_ID int NOT NULL,
Product_Size char(1) NOT NULL,
PRIMARY KEY (Size_ID)
);

create table Product_Sizes(
Product_Size_ID int NOT NULL auto_increment,
Product_ID int NOT NULL,
Size_ID int NOT NULL,
Price decimal NOT NULL,
PRIMARY KEY (Product_Size_ID),
FOREIGN KEY (Product_ID) REFERENCES Products(Product_ID),
FOREIGN KEY (Size_ID) REFERENCES Sizes(Size_ID)
);

create table Order_Details(
Order_Details_ID int auto_increment,
Order_ID int NOT NULL,
Product_Size_ID int NOT NULL,
Quantity int NOT NULL,
Amount decimal NOT NULL,
PRIMARY KEY (Order_Details_ID),
FOREIGN KEY (Order_ID) REFERENCES Orders(Order_ID),
FOREIGN KEY (Product_Size_ID) REFERENCES Product_Sizes(Product_Size_ID)
);

create table Update_Details(
Update_ID int NOT NULL auto_increment,
Product_ID int NOT NULL,
Create_By int NOT NULL default '265204',
Create_Time datetime default current_timestamp(),
Update_By int,
Update_Time datetime,
Update_Des varchar(300),
PRIMARY KEY(Update_ID),
FOREIGN KEY (Product_ID) REFERENCES Products(Product_ID),
FOREIGN KEY (Update_By) REFERENCES Staffs(Staff_ID)
);

-- INSERT STAFF INFO TO TABLE "STAFFS"
insert into Staffs(Staff_ID, Staff_Name, User_Name, Password, Staff_Status)
value ('265204','Nguyen Ngoc Duc', 'duckhongngu', 'A067A668279CD1BEA956DF8EF006C895', '1');
insert into Staffs(Staff_ID, Staff_Name, User_Name, Password, Staff_Status)
value ('111111','Nguyen Thi Khanh Ly', 'lykhongngu', '1F0076B07D772FA103AB9B95CE50B479', '1');
insert into Staffs(Staff_ID, Staff_Name, User_Name, Password, Staff_Status)
value ('22222', 'Ngu', '1', 'C4CA4238A0B923820DCC509A6F75849B', '1');

-- INSERT PRODUCT INFO TO TABLE "PRODUCTS"
insert into Products(Product_ID, Product_Name, Descriptions)
value ('1', 'Capuchino','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('2', 'Ice Tea','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('3', 'Latte','');

-- INSERT SIZE INFO TO TABLE "SIZES'
insert into Sizes(Size_ID, Product_Size)
value ('1','S');
insert into Sizes(Size_ID, Product_Size)
value ('2','M');
insert into Sizes(Size_ID, Product_Size)
value ('3','L');

-- INSERT PRODUCT SIZE INFO TO TABLE "PRODUCT_SIZES"
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('1','1','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('1','2','23000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('1','3','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('2','1','10000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('2','2','12000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('2','3','15000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('3','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('3','2','32000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('3','3','35000');

-- INSERT UPDATE OF PRODUCTS TO TABLE "UPDATE_DETAILS"
insert into update_details(Product_ID, Update_Des)
value ('1', 'Create Product Capuchino');

insert into update_details(Product_ID, Update_Des)
value ('2', 'Create Product Ice Tea');

insert into update_details(Product_ID, Update_Des)
value ('3', 'Create Product Latte');

-- CREATE AN SUB ACCOUNT TO DATABASE 
CREATE USER IF NOT exists 'nguyenngocduc'@'localhost' IDENTIFIED BY 'Duc265204@';
GRANT ALL PRIVILEGES ON coffeeshop.* TO 'nguyenngocduc'@'localhost' WITH GRANT OPTION;


