Create procedure [dbo].[SearchPatient]  
(  
   @query nvarchar (40)  
)  
as   
begin  
   SELECT * FROM PatientTable WHERE name LIKE (@query); 
End