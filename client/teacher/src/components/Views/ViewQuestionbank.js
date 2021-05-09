import React, {useState, useRef, useEffect} from 'react';
import axios from 'axios';
import { createMuiTheme, withStyles, makeStyles, ThemeProvider  } from '@material-ui/core/styles'
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import InputBase from '@material-ui/core/InputBase';
import SearchIcon from '@material-ui/icons/Search';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card'
import CardActionArea from '@material-ui/core/CardActionArea'
import CardActions from '@material-ui/core/CardActions'
import CardContent from '@material-ui/core/CardContent'
import CardMedia from '@material-ui/core/CardMedia'
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import Checkbox from '@material-ui/core/Checkbox';
import RadioGroup from '@material-ui/core/RadioGroup';
import { dictOfUniverse, dictOfSolar, dictOfPlanet } from './../../variables/general';
import Link from '@material-ui/core/Link'


// const ViewQuestionbank = (props) => {
//     return(
// <div>
//         <NewQuestion/>
//     </div>
//     )
    
// }



function createData(universe,solar, planet, questionID, question) {
    return { Universe: dictOfUniverse[universe-1], Solar: dictOfSolar[solar-1], Planet: dictOfPlanet[planet-1], QuestionID: questionID, question};
  }

    const ViewQuestionbank = () => {
      const token = sessionStorage.getItem('token');
      console.log("token = view()", token);
        const [fullDataQuestions, setFullData] = React.useState({});
        const [inputState, setInput] = React.useState({});
        useEffect(() => {
          console.log(inputState) // do something after state has updated
        }, [inputState]);
        const [questions, setQuestions] = useState([]);
        const [loading, setLoading] = useState(false);
        const [isModalOpen, setIsModalOpen] = useState(false);
        const [optionA,setA] = useState('');
        const [optionB,setB] = useState('');
        const [optionC,setC] = useState('');
        const [optionD,setD] = useState('');
        const [page, setPage] = useState(0);
        
        const [rowsPerPage, setRowsPerPage] = useState(3);
        // const [correct,setCorrect] = useState('');
        const [currentQID, setCurrentQID]=useState(-1);
        const getOptionFromIndex = (val) => {
          switch (val) {
            case "0": console.log(optionA);return optionA;
            case "1": console.log(optionB);return optionB;
            case "2": console.log(optionC);return optionC;
            case "3": console.log(optionD);return optionD;
          }
          return "";
        }

        const fetchQuestions = () => {
                var myHeaders = new Headers();
                myHeaders.append("Authorization", "Bearer "+token);
                fetch('http://localhost:5000/teacher/question',{headers: myHeaders})
                .then(response => response.json())
                .then(data => setQuestions(data))
                .then(setLoading(true));
                 
        }
        const fetchFullDataQuestions = (qid) => {
          var myHeaders = new Headers();
          myHeaders.append("Authorization", "Bearer "+token);

var raw = "";

var requestOptions = {
  method: 'GET',
  headers: myHeaders,
  redirect: 'follow'
};

fetch("http://localhost:5000/teacher/question/"+String(qid), requestOptions)
  .then(response => response.json())
  .then(data => {setFullData(data); setInput(data);setA(data.correctOption);setB(data.wrongOptions[0]);setC(data.wrongOptions[1]);setD(data.wrongOptions[2])})
  .then(setLoading(true))
  .then(result => setEditOpen(true))
  .catch(error => console.log('error', error));
          
           
  }
        

        const handleChange = (event) => {
          console.log('target val',event.target.value);
            switch(event.target.name){
              case "OptionA":
                setA(event.target.value);
                break;
              case "OptionB":
                setB(event.target.value);
                break;
              case "OptionC":
                setC(event.target.value);
                break;
              case "OptionD":
                setD(event.target.value);
                break;
              default:
                if (event.target.name == "body" || event.target.name == "correctOption" )
                  {inputState[event.target.name] = event.target.value;}
                else
                  {inputState[event.target.name] = Number(event.target.value);}
                break;
            }
            
            setInput(inputState);
          };
          // Add dialog
        const [AddOpen, setAddOpen] = React.useState(false);
        function DelDialog(){
          return(
            <div>
                      <Dialog open={DeleteOpen} onClose={handleDeleteCancel} aria-labelledby="form-dialog-title" maxWidth='xl'>
                        <DialogTitle id="form-dialog-title" color='primary'>Delete Question</DialogTitle>
                        <DialogContent>Are you sure you want to delete this question?</DialogContent>
                        <DialogActions>
                          <Button onClick={handleDeleteCancel} color="primary">
                            Cancel
                          </Button>
                          <Button onClick={() => handleDeleteOk()} color="primary">
                            Yes Delete
                          </Button>
                        </DialogActions>
                      </Dialog>
          </div>
          )
          
        }
        function editDialog(){
          if (!inputState.wrongOptions)
          {
            return(<div/>)
          }
          else{
            return(
              <div>
                    <div>
                      <Dialog open={EditOpen} onClose={handleEditCancel} aria-labelledby="form-dialog-title" maxWidth='xl'>
                          <DialogTitle id="form-dialog-title" color='primary'>Edit Question</DialogTitle>
                          <DialogContent>
                          <form  noValidate autoComplete="off">
                            <TextField name = "universeID" id="standard-basic" label="Universe" required="true" style = {{width: '45%'}} defaultValue= {inputState.universeID} onChange={handleChange}/>
                            <TextField name = "solarID" id="standard-basic" label="Solar" required="true" style = {{width: '45%'}}  defaultValue={inputState.solarID} onChange={handleChange}/>
                            <TextField name = "planetID" id="standard-basic" label="Planet" required="true" style = {{width: '45%'}} defaultValue={inputState.planetID} onChange={handleChange}/>
                            {/* <TextField name = "questionID" id="standard-basic" label="Planet question ID" fullWidth="true" required="true" style = {{width: '91%'}} defaultValue={inputState.questionID} onChange={handleChange}/> */}
                            <TextField name = "body" id="standard-basic" label="Planet question" fullWidth="true" required="true" style = {{width: '91%'}} defaultValue={inputState.body} onChange={handleChange}/>
                            <Typography variant="h7" >
                            <br></br> {"     "}Select the Checkbox with the correct option:
                            </Typography>
                            <div >
  
                            <Checkbox
                              defaultChecked = {true}
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="0"
                              onChange={checkAns}
                            />
                            <TextField name = "OptionA" id="standard-basic" label="Option 1" required="true" style = {{width: '90%'}} defaultValue={inputState.correctOption} onChange={handleChange}/>
                            </div>
                            <div >
                            <Checkbox
                              
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="1"
                              onChange={checkAns}
                            />
                            <TextField name = "OptionB" id="standard-basic" label="Option 2" required="true" style = {{width: '90%'}} defaultValue={inputState.wrongOptions[0]} onChange={handleChange}/>
                            </div>
                            <div>
                            <Checkbox
                              
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="2"
                              onChange={checkAns}
                            />
                            <TextField name = "OptionC" id="standard-basic" label="Option 3" required="true" style = {{width: '90%'}} defaultValue={inputState.wrongOptions[1]} onChange={handleChange}/>
                            </div>
                            <div>
                            <Checkbox
                              
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="3"
                              onChange={checkAns}
                            />
                            <TextField name = "OptionD" id="standard-basic" label="Option 4" required="true" style = {{width: '90%'}} defaultValue={inputState.wrongOptions[2]} onChange={handleChange}/>
                            </div>
                          </form>
                          </DialogContent>
                          <DialogActions>
                            <Button onClick={handleEditCancel} color="primary">
                              Cancel
                            </Button>
                            <Button onClick={handleEditOk} color="primary">
                              Update Question
                            </Button>
                          </DialogActions>
                        </Dialog>
                  
                      </div>
                      </div>
            )
          }
            
                    }
     
        const toggleAddModal = () => {
            const newObj = {
              universeID: 1,
              solarID: 1,
              planetID: 1,
              questionID: 1,
              body: "",
              correctOption: "0",
              wrongOptions:["1", "2", "3"],
            }
            setAddOpen(true);
            setInput(newObj);
            
          };
        useEffect(()=>{
            fetchQuestions()
        },[setFullData])
        const [updated, setUpdate] = useState(true);
        const handleAddOk = () => {
          // if(inputState.correctOption===0)
          // {
          //   setInput({
          //     ...inputState, 
          //     correctOption: getOptionFromIndex(inputState.correctOption),
          //    wrongOptions: inputState.wrongOptions.map(getOptionFromIndex)
          //  })
          // }
            // console.log('Input',inputState);
             
            //TODO: Update DB question 
            var myHeaders = new Headers();
            myHeaders.append("Authorization", "Bearer "+token);
            myHeaders.append("Content-Type", "application/json");
            
            var raw = JSON.stringify(inputState);
            
            var requestOptions = {
              method: 'POST',
              headers: myHeaders,
              body: raw,
              redirect: 'follow'
            };
            
            fetch("http://localhost:5000/teacher/question/"+String(inputState.questionID), requestOptions)
              .then(response => response.text())
              .then(result => {console.log(result);setQuestions([...questions,inputState].sort((a, b) => a.questionID - b.questionID))})
              .catch(error => console.log('error', error));

            setUpdate(true);
            setAddOpen(false);
          };
        
          const handleAddCancel = () => {
              setAddOpen(false);
          };
            // Edit dialog
        const [EditOpen, setEditOpen] = React.useState(false);
    
        const toggleEditModal = (index) => {

        fetchFullDataQuestions(index);
        // console.log(questions);
        // setInput(fullDataQuestions);
        // console.log('new input',inputState);
        // setEditOpen(true);
        // console.log('index',index)
        // {console.log('button id',event.target.id)}
        };

        const [DeleteOpen, setDeleteOpen] = React.useState(false);
  
      const toggleDeleteModal = (qid) => {
        
       setCurrentQID(qid);
       setDeleteOpen(true);
      }
  
      const handleDeleteCancel = () => {
        setDeleteOpen(false);
      }
  
    const handleDeleteOk = () => {
      var myHeaders = new Headers();
      myHeaders.append("Authorization", "Bearer "+token);


      var requestOptions = {
        method: 'DELETE',
        headers: myHeaders,
        redirect: 'follow'
      };

      fetch("http://localhost:5000/teacher/question/"+String(currentQID), requestOptions)
        .then(response => response.text())
        .then(result => {console.log(result); setDeleteOpen(false);
        setUpdate(true);
        setQuestions(questions.filter((question) => question.questionID !== currentQID));})
        .catch(error => console.log('error', error));
      
          }

    const handleEditOk = () => {
      
                 inputState.correctOption = optionA;
                 inputState.wrongOptions=[];
                 inputState.wrongOptions.push(optionB);
                 inputState.wrongOptions.push(optionC);
                 inputState.wrongOptions.push(optionD);
            
            setInput(inputState);
            
            var myHeaders = new Headers();
            myHeaders.append("Authorization", "Bearer "+token);
            myHeaders.append("Content-Type", "application/json");
            
            var raw = JSON.stringify(inputState);
            var requestOptions = {
              method: 'PUT',
              headers: myHeaders,
              body: raw,
              redirect: 'follow'
            };
            
            fetch("http://localhost:5000/teacher/question/"+String(inputState.questionID), requestOptions)
              .then(response => response.json())
              .then(result => {console.log(result);setQuestions([...questions.filter((question) => question.questionID !== inputState.questionID), inputState].sort((a, b) => a.questionID - b.questionID));})
              .catch(error => console.log('error', error));
        setEditOpen(false);
        setUpdate(true);
      };
    
      const handleEditCancel = () => {
          setEditOpen(false);
          setInput({});
      };

      const checkAns = (event) => {
        const isChecked = event.target.checked;
        console.log(isChecked)
        if(isChecked){
          setInput({...inputState, 
            correctOption: getOptionFromIndex(event.target.name), 
            wrongOptions: ["0", "1", "2", "3"].filter(val => {
              return val !== event.target.name;
            }).map(getOptionFromIndex)
          });
          // setCorrect(event.target.name);
        }
        else {
          setInput({...inputState, 
            correctOption: "", 
            wrongOptions: ["0", "1", "2", "3"].map(getOptionFromIndex)
          });
        }
        
      }
    
  
        // const [page, setPage] = useState(0);
        // const [rowsPerPage, setRowsPerPage] = useState(5);
    
      const handleChangePage = (e, newPage) => {
        setPage(newPage);
      };
    
    
      const handleChangeRowsPerPage = (e) => {
        setRowsPerPage(+e.target.value);
        setPage(0);
      };
      

    const ColorButton = withStyles((theme) => ({
        root: {
            body :{
                color: 'white'
              },
            backgroundColor: '#6f7bd9',
            margin:'auto',
            marginRight:'2rem'
        }
      }))(Button); 
    const IColourButton = withStyles((theme) => ({
        root: {
            body :{
                color: 'white'
              },
            backgroundColor: '#6f7bd9',
            
          marginLeft:'49rem'
        }
        }))(IconButton);  
    function toggleModal(){
        setIsModalOpen(true);
    }
    
    // console.log("render state:",questions);
    return(
            <div className='questionBank-container'>
            <h1 >Question Bank </h1>
            <br/>
            <Button color="primary" round onClick={toggleAddModal}  >Add new question</Button>
            {questions.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((ques)=>(
            <Card>
                <CardActionArea>
                    <CardMedia title="Question Bank"/>
                  
                    <CardContent><Typography gutterBottom variant="h5" component="h2">
                                  Q{ques.questionID} <br/>
                                  {ques.body}
                                  </Typography>

                        <Typography>
                            A: {ques.correctOption}   <br/>
                            B: {ques.wrongOptions[0]} <br/>
                            C: {ques.wrongOptions[1]} <br/>
                            D: {ques.wrongOptions[2]} <br/> 
                        </Typography>
                    </CardContent>
                </CardActionArea>
                <CardActions>
                    <ColorButton size="large" variant="outlined" onClick={() => toggleEditModal(ques.questionID)}>
                            Edit
                    </ColorButton>
                      {editDialog(ques.questionID)}
                    <ColorButton size="large" variant="outlined" onClick = {() => toggleDeleteModal(ques.questionID)} >
                            Delete
                    </ColorButton>
                      {DelDialog()}
                </CardActions>
            </Card>
            ))}
            
            <Dialog open={AddOpen} onClose={handleAddCancel} aria-labelledby="form-dialog-title" maxWidth='xl'>
                          <DialogTitle id="form-dialog-title" color='primary'>Add Question</DialogTitle>
                          <DialogContent>
                          <form  noValidate autoComplete="off">
                            <TextField name = "universeID" type="number" id="standard-basic" label="Universe" required="true" style = {{width: '45%'}} onChange={handleChange}/>
                            <TextField name = "solarID" type="number" id="standard-basic" label="Solar" required="true" style = {{width: '45%'}} onChange={handleChange}/>
                            <TextField name = "planetID" type="number" id="standard-basic" label="Planet" required="true" style = {{width: '45%'}} onChange={handleChange}/>
                            <TextField name = "questionID" type="number" id="standard-basic" label="QuestionID" required="true" style = {{width: '45%'}} onChange={handleChange}/>
                            <TextField name = "body" id="standard-basic" label="Planet Question" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChange}/>
                            <Typography variant="h7" >
                            <br></br> {"     "}Select the Checkbox with the correct option:
                            </Typography>
                            <div  >
                            <Checkbox
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="0"
                              onChange={checkAns}
                            />
                            <TextField id="standard-basic" label="Option 1" required="true" style = {{width: '90%'}} name = "OptionA" onChange={handleChange}/>
                            </div>
                            <div  >
                            <Checkbox
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="1"
                              onChange={checkAns}
                            />
                            <TextField id="standard-basic" label="Option 2" required="true" style = {{width: '90%'}} name = "OptionB" onChange={handleChange}/>
                            </div>
                            <div >
                            <Checkbox
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="2"
                              onChange={checkAns}
                            />
                            <TextField id="standard-basic" label="Option 3" required="true" style = {{width: '90%'}} name = "OptionC" onChange={handleChange}/>
                            </div>
                            <div  >
                            <Checkbox
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="3"
                              onChange={checkAns}
                            />
                            <TextField id="standard-basic" label="Option 4" required="true" style = {{width: '90%'}} name = "OptionD" onChange={handleChange}/>
                            </div>
  
                          </form>
                          </DialogContent>
                          <DialogActions>
                            <Button onClick={handleAddCancel} color="primary">
                              Cancel
                            </Button>
                            <Button onClick={handleAddOk} color="primary">
                              Update Quiz
                            </Button>
                            
                          </DialogActions>
                        </Dialog>
                        
                
            <TablePagination
            rowsPerPageOptions={[3,5,10,25,100]}
            component="div"
            count={questions.length}
            rowsPerPage={rowsPerPage}
            page={page}
            onChangePage={handleChangePage}
            onChangeRowsPerPage={handleChangeRowsPerPage}/>
        
</div>
)
            }

export default ViewQuestionbank