# Grifindo-Toys-payroll-system


Grifindo Toys is a small-scale Toy building company which is located in United Kingdom (UK) and currently they have 50 employees working at their headquarters. They are looking for a simple payroll system to calculate the salaries of their employees and you are hired as a freelance software developer to build their payroll system. 
Specifications for the payroll system as follows,
                                 

 Grifindo Toys Payroll System mainly contains five components and the specifications for the     components are follows,
1.	Employee Component. 
•	Admin should be able to register employee details to the system (including monthly salary, overtime rates-hourly, allowances).
•	Admin should be able to update all employee details in the system (Update and Delete including monthly salary, overtime rates-hourly, allowances).
•	Admin should be able to view individual employee details, view all employees details, search employees.
2.	Salary Component 
	Admin should be able to input the date range to calculate the salary. Salary cycle begin date and the end date should be given to calculate the monthly salary. Salary cycle begin date and end date will be defined in the settings component and if the user enters something else the system should display an error message.

	The admin should be able to enter the no of leaves an employee has taken with number of absent days, no of holidays for the given date range. If an employee has worked any overtime hours the admin should be able to enter that also when calculating the Base pay value. 




                      Base Pay need to calculate based on the following points,
Base Pay (Monthly Salary: salary_cycle_date_range, eg: 30 days): Each employee will be paid monthly 
If any employee couldn’t cover salary_cycle_date_range (eg:-30 days) attendance the system should calculate the no-pay value according to the below mention formula,
 
No-pay-value = (Monthly_Salary /salary_cycle_date_range) *No_of_absent_days

Base Pay need to calculate according to the below mentioned formula
Base Pay value = Monthly_Salary + Allowances + (Over_time_rate*no_of_overtime_hours)
Gross Pay need to calculate according to the below mentioned formula
Gross Pay = Base_Pay_value – (No_pay_value + Base_Pay_value*government_tax_rate)

All the calculated No-pay-value, Base-pay-value and Gross pay value should record in the database under each employee for each month. This component should generate reports such as monthly salary report for an employee, overall salary summary for couple of months for an employee, No-pay-value, base-pay-value, and gross pay value of all the employees for a given month range.

3.	Settings Component 
               This component should allow the admin to change parameter values such as
i.	Date Range for a salary cycle. Eg:- 30 days
ii.	Salary cycle begin date
iii.	Salary cycle end date
iv.	No of leaves for an employee for a year.
