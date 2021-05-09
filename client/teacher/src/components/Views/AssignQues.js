import React from 'react';
import TextField from '@material-ui/core/TextField';

const AssignQues = (handleChangeQuestion) => {
    return(
        <div>
        <br/>
        <form  noValidate autoComplete="off">
        
        <TextField name = "questionID" type="number" id="standard-basic" label="QuestionID" required="true" style = {{width: '45%'}} onChange={handleChangeQuestion}/>
        <TextField name = "body" id="standard-basic" label="Assignment Question" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChangeQuestion}/>
        <TextField name = "correctOption" id="standard-basic" label="Correct Option" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChangeQuestion}/>
        <TextField name = "wrongOptions[0]" id="standard-basic" label="Wrong option 1" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChangeQuestion}/>
        <TextField name = "wrongOptions[1]" id="standard-basic" label="Wrong option 2" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChangeQuestion}/>
        <TextField name = "wrongOptions[2]" id="standard-basic" label="Wrong option 3" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChangeQuestion}/>
      </form>
      </div>
    )
}

export default AssignQues;


