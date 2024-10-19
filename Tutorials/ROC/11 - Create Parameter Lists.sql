DECLARE @parameterArray [ROCMaster].[ParameterArray] = [ROCMaster].[ParameterArray]::Empty();

SET @parameterArray = @parameterArray.AddParameter([ROCMaster].[Parameter]::ParseAc12(3,0,0,'Hello World')).AddParameter([ROCMaster].[Parameter]::ParseInt32(3,0,0,69));

PRINT @parameterArray.GetParameter(0).ToString();
PRINT @parameterArray.GetParameter(1).ToString()

