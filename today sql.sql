Create DAtabase Grifindore_DB;
USE Grifindore_DB;

create table employee (

eid varchar(5) primary key,
name varchar(50),
address varchar(100),
dob date,
bsal decimal(10,2),
otrate decimal(10,2),
allowance int
);

create table salCycle(
year int,
month int,
salcycleStart date,
salCycleend date,
noDays int,
taxRate decimal (10,2),

constraint salCyclepk primary key (year,month)
);

create table leaves(
year int,
leaves int,
cycle_range int,
start_dt date,
end_dt date

);

create table salary
(
year int,
month int,
eid varchar(5),
OThrs decimal(10,2),
abDays decimal(10,2),
netSal decimal(10,2),

constraint salarypk primary key (year,month,eid)
);