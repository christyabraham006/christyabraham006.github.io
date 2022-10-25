Create procedure [dbo].[UpdatePatient]  
(  
   @StdId int,
   @name nvarchar (40),    
   @Address nvarchar (40),
   @Age int,
   @In_or_Out bit,
   @Deleted bit,
   @Doctor nvarchar (20)
)  
as  
begin  
   Update PatientTable   
   set name=@name,  
   Age=@Age,  
   Address=@Address,
   Doctor=@Doctor,
   In_or_Out=@In_or_Out,
   [Delete]=0
   where Id=@StdId  
End