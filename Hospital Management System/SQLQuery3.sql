Create procedure [dbo].[DeletePatient]  
(  
   @StdId int  
)  
as   
begin  
   Update PatientTable   
   set [Delete]=0 where Id=@StdId  
End