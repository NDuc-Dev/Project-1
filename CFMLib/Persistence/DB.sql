DROP database if exists coffeeshop;
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
Product_ID int NOT NULL,
Size_ID int NOT NULL,
Quantity int NOT NULL,
Amount decimal NOT NULL,
PRIMARY KEY (Order_Details_ID),
FOREIGN KEY (Order_ID) REFERENCES Orders(Order_ID),
FOREIGN KEY (Size_ID) REFERENCES Product_Sizes(Size_ID)
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
value ('2', 'Coconut Juice','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('3', 'Latte','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('4', 'Traditonal Coffee','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('5', 'Milk Tea','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('6', 'Orange Lemongrass Peach Tea','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('7', 'Lemon Tea','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('8', 'Kumquat Tea','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('9', 'Peach Tea With Jelly','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('10', 'Lychee Tea With Jely','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('11', 'Espresso','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('12', 'Macchiato','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('13', 'Mocha','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('14', 'Hot chocolate','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('15', 'Strawberry smoothie','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('16', 'Milk Espresso','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('17', 'Matcha Ice Blended','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('18', 'Peach Pomelo Tea','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('19', 'Ginger Tea','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('20', 'Mango Smoothie','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('21', 'Hot White Chocolate','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('22', 'Iced Yogurt','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('23', 'Caramel Macchiato','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('24', 'Olong Tea With Lychee','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('25', 'Matcha Macchiato','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('26', 'Matcha Latte','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('27', 'Blueberry Smoothie','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('28', 'Coffee Ice Blended','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('29', 'Cold Brew','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('30', 'Cookie Ice Blended','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('31', 'Milk Tea Latte','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('32', 'Choco Caramel Ice Blended','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('33', 'Banana Yogurt Smoothies','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('34', 'Passion Yogurt Smoothies','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('35', 'Watermelon Juices','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('36', 'Orange Juice','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('37', 'Strawberry Smoothie','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('38', 'Pineapple Squash','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('39', 'Coffee Milkshake','');
insert into Products(Product_ID, Product_Name, Descriptions)
value ('40', 'Apricot Juice','');

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
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('4','1','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('4','2','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('4','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('5','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('5','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('5','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('6','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('6','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('6','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('7','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('7','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('7','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('8','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('8','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('8','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('9','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('9','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('9','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('10','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('10','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('10','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('11','1','18000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('11','2','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('11','3','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('12','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('12','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('12','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('13','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('13','2','32000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('13','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('14','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('14','2','32000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('14','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('15','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('15','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('15','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('16','1','15000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('16','2','18000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('16','3','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('17','1','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('17','2','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('17','3','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('18','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('18','2','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('18','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('19','1','15000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('19','2','18000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('19','3','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('20','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('20','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('20','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('21','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('21','2','32000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('21','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('22','1','15000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('22','2','18000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('22','3','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('23','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('23','2','32000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('23','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('24','1','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('24','2','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('24','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('25','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('25','2','32000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('25','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('26','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('26','2','32000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('26','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('27','1','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('27','2','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('27','3','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('28','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('28','2','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('28','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('29','1','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('29','2','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('29','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('30','1','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('30','2','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('30','3','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('31','1','25000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('31','2','28000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('31','3','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('32','1','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('32','2','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('32','3','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('33','1','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('33','2','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('33','3','45000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('34','1','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('34','2','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('34','3','45000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('35','1','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('35','2','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('35','3','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('36','1','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('36','2','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('36','3','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('37','1','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('37','2','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('37','3','45000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('38','1','30000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('38','2','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('38','3','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('39','1','33000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('39','2','35000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('39','3','40000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('40','1','20000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('40','2','23000');
insert into Product_Sizes(Product_ID, Size_ID, Price)
value ('40','3','25000');

-- INSERT UPDATE OF PRODUCTS TO TABLE "UPDATE_DETAILS"
insert into update_details(Product_ID, Update_Des)
value ('1', 'Create Product Capuchino');

insert into update_details(Product_ID, Update_Des)
value ('2', 'Create Product Ice Tea');

insert into update_details(Product_ID, Update_Des)
value ('3', 'Create Product Latte');

insert into update_details(Product_ID, Update_Des)
value ('3', 'Create Product Traditional Coffee');

insert into update_details(Product_ID, Update_Des)
value ('5', 'Create Product Milk Tea');



-- CREATE AN SUB ACCOUNT TO DATABASE 
CREATE USER IF NOT exists 'nguyenngocduc'@'localhost' IDENTIFIED BY 'Duc265204@';
GRANT ALL PRIVILEGES ON coffeeshop.* TO 'nguyenngocduc'@'localhost' WITH GRANT OPTION;