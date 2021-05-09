import React, { useState,useEffect,useCallback  } from 'react';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { produce } from 'immer';
import Button from '@material-ui/core/Button'
import AddTwoToneIcon from '@material-ui/icons/AddTwoTone'
import TablePagination from '@material-ui/core/TablePagination'
import IconButton from '@material-ui/core/IconButton'
import { Link } from 'react-router-dom';
import { withStyles } from '@material-ui/core/styles'
import Card from '@material-ui/core/Card'
import CardActionArea from '@material-ui/core/CardActionArea'
import CardActions from '@material-ui/core/CardActions'
import CardContent from '@material-ui/core/CardContent'
import CardMedia from '@material-ui/core/CardMedia'
import Typography from '@material-ui/core/Typography'
import {Linking } from 'react-native';
import { TwitterIcon, RedditIcon } from 'react-share';
import useToken from '../../variables/useToken';
const ViewAssignment = () => {
    console.log("Entering  view");
    const token = sessionStorage.getItem('token');
    console.log("token = view()", token);
    const [assignments, setAssignments] = useState([])
    const [arr, setArr] = useState([0, 1, 2]);
    const [AddOpen, setAddOpen] = React.useState(false);
    const [inputState, setInput] = React.useState({});
    const [updated, setUpdate] = useState(true);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(3);
    const [questions, setQuestions] = useState([
        {
            points: 0,
            questionID: 0,
            body: "",
            correctOption: "",
            wrongOptions: [0,1,2]
        }
    ]);

    const toggleAddModal = () => {
        const newObj = [
          {
            assignmentName:"",
            timeLimit:1,
            questionIDs:[],
            deadline:"",
            tutGrp:""
          }
        ]
        setAddOpen(true);
        setInput(newObj);

      };

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

    String.format = function() {
    var theString = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }
    return theString;
  }
  function tweetNow(assignment){
    let twitterParameters = [];
    let tweetContent = String.format("Shared Assignment {0}\nName: {1}\nDuration: {2}\nDeadline: {3}", assignment.assignmentID, assignment.assignmentName, assignment.timeLimit, assignment.deadline);
    twitterParameters.push('text=' + encodeURI(tweetContent));
    //twitterParameters.push('url=' + encodeURI(twitterShareURL));  
    //twitterParameters.push('via=' + encodeURI(twitterViaAccount));
    const url =
      'https://twitter.com/intent/tweet?'
      + twitterParameters.join('&');
    Linking.openURL(url);
  };
  
  function redditNow(assignment){
    let redditParameters = [];
    let redditContent = String.format("Shared Assignment {0}\nName: {1}\nDuration: {2}\nDeadline: {3}", assignment.assignmentID, assignment.assignmentName, assignment.timeLimit, assignment.deadline);
    redditParameters.push('text=' + encodeURI(redditContent));
    redditParameters.push('title=' + encodeURI("Softvengers assignment"));
    const url =
      'http://reddit.com/submit?'
      + redditParameters.join('&');
    Linking.openURL(url);
  };
  
   
    const handleChange = (event) => {
        console.log('target val', event.target.value);

        if (event.target.name == "assignmentName" || event.target.name == "tutGrp" || event.target.name == "deadline") { inputState[0][event.target.name] = event.target.value; }
        else { inputState[0][event.target.name] = Number(event.target.value); }
        setInput(inputState);
    }

    const fetchAssignments = async ()=> {
        var myHeaders = new Headers();
        console.log("token"+token);
        myHeaders.append("Authorization", "Bearer "+token);
        const res = await fetch('http://localhost:5000/teacher/assignment/list/SCE5',{
          method: 'GET',
          headers: myHeaders,
          redirect: 'follow'
        })
        .catch(error => console.log('error', error));
        const data = await res.json()
        console.log(data);
        return data
      }
      useEffect(()=>{
        const getAssignments = async()=>{
          const assignementsFromServer = await fetchAssignments()
          
          setAssignments(assignementsFromServer)
    
        }
        getAssignments()
      },[setAssignments])

    const handleAddOk = () => {
        console.log('Input', inputState);
        // handleAddQuestionok();
        questions.map(i => {
            inputState.push(i);
            inputState[0].questionIDs.push(i.questionID);
        })
        setInput(inputState);
        assignments.push(inputState[0]);
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

        fetch("http://localhost:5000/teacher/assignment/" + String(inputState[0].assignmentID), requestOptions)
            .then(response => response.text())
            .then(result => {console.log(result);setQuestions([...assignments,inputState].sort((a, b) => a.assignmentID- b.assignmentID));})
            .catch(error => console.log('error', error));

        setUpdate(true);
        setAddOpen(false);
    };

    const handleAddCancel = () => {
        setAddOpen(false);
    };


    return (
        
        <div className='assignment-container'>
            {console.log('returning ...')}
            <h1>Assignments <IColourButton><AddTwoToneIcon onClick={toggleAddModal} /> </IColourButton></h1>
            {assignments.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((assignment) => (
                <Card>
                    <CardActionArea>
                        <CardMedia title="Assignment" />
                        <CardContent><Typography gutterBottom variant="h5" component="h2">Assignment {assignment.assignmentID}</Typography>
                            <Typography>{assignment.assignmentName}</Typography>
                            <Typography>Due : {assignment.deadline}</Typography>
                            <Typography>Timelimit: {assignment.timeLimit}</Typography>
                        </CardContent>
                    </CardActionArea>
                    <CardActions>
                    <ColorButton onClick={() => tweetNow(assignment)} > 
                    <TwitterIcon size={32} round={true} /> Share on Twitter</ColorButton>
                    <ColorButton onClick={() => redditNow(assignment)}> 
                    <RedditIcon size={32} round={true} /> Share on Reddit</ColorButton>
                    <ColorButton size="large" variant="outlined" ><Link style={{ textDecoration: 'none' }}to={`Assignmentpage/${assignment.assignmentID}`}>Statistics</Link></ColorButton>
                    </CardActions>
                </Card>))}
            <Dialog open={AddOpen} onClose={handleAddCancel} aria-labelledby="form-dialog-title" maxWidth='xl'>
                <DialogTitle id="form-dialog-title" color='primary'>Add Assignment</DialogTitle>
                <DialogContent>
                    <form noValidate autoComplete="off">

                        <TextField name="assignmentID" type="number" id="standard-basic" label="Assignment ID" required="true" style={{ width: '45%' }} onChange={handleChange} />
                        <TextField name="assignmentName" id="standard-basic" label="Assignment Name" required="true" style={{ width: '45%' }} onChange={handleChange} />
                        <TextField name="timeLimit" type="number" id="standard-basic" label="Time limit" required="true" style={{ width: '45%' }} onChange={handleChange} />
                        <TextField name="deadline" type="date" id="standard-basic" fullWidth="true" required="true" style={{ width: '91%' }} onChange={handleChange} />
                        <TextField name="tutGrp" id="standard-basic" label="Tutorial Group" fullWidth="true" required="true" style={{ width: '91%' }} onChange={handleChange} />
                    </form>
                </DialogContent>
                <DialogActions>
                
                    {questions.map((q, index) => {
                        return (
                            <div key={index}>

                                <TextField onChange = {(e)=>{
                                    const qid = e.target.value;
                                    setQuestions(currQid => 
                                        produce(currQid,(v)=>{
                                            v[index].questionID = Number(qid);
                                        })
                                        );
                                }}
                                placeholder = "Question ID"/>

                                

                                <TextField onChange={(e) => {
                                    const points = e.target.value;
                                    setQuestions(currPoint =>
                                        produce(currPoint, (v) => {
                                            console.log('produce call');
                                            v[index].points = Number(points);
                                        })
                                    );
                                }}
                                    placeholder="Points" />

                                <TextField onChange = {(e)=>{
                                    const body = e.target.value;
                                    setQuestions(currBody => 
                                        produce(currBody,(v)=>{
                                            v[index].body = body;
                                        })
                                        );
                                }}
                                placeholder = "Body"/>

                                <TextField onChange = {(e)=>{
                                    const correct = e.target.value;
                                    setQuestions(currCorrect => 
                                        produce(currCorrect,(v)=>{
                                            v[index].correctOption = correct;
                                        })
                                        );
                                }}
                                placeholder = "Correct Option"/>
                                
                                <TextField onChange = {(e)=>{
                                    const wrong1 = e.target.value;
                                    setQuestions(currWrong1 => 
                                        produce(currWrong1,(v)=>{
                                            v[index].wrongOptions[0] = wrong1;
                                        })
                                        );
                                }}
                                placeholder = "Wrong1"/>


                                    <TextField onChange = {(e)=>{
                                    const wrong2 = e.target.value;
                                    setQuestions(currWrong2 => 
                                        produce(currWrong2,(v)=>{
                                            v[index].wrongOptions[1] = wrong2;
                                        })
                                        );
                                }}
                                placeholder = "wrong2"/>

                                    <TextField onChange = {(e)=>{
                                    const wrong3 = e.target.value;
                                    setQuestions(currWrong3 => 
                                        produce(currWrong3,(v)=>{
                                            v[index].wrongOptions[2] = wrong3;
                                        })
                                        );
                                }}
                                placeholder = "wrong3"/>

                                
                            </div>
                        )
                    })}
                    <Button onClick={() => {
                        setQuestions(currQuestions => [
                            ...currQuestions,
                            {
                                wrongOptions: [0,1,2],
                                points: 0,
                                questionID: 0,
                                body: "",
                                correctOption: ""
                            }
                        ])
                    }} color = "primary"> Add more ques </Button>
                    <Button onClick={handleAddCancel} color="primary">
                              Cancel
                            </Button>
                    <Button onClick={handleAddOk} color="primary">
                        Submit Assignment
                            </Button>
                    
                </DialogActions>
            </Dialog>
            <TablePagination
        rowsPerPageOptions={[3,5,10,25,100]}
        component="div"
        count={assignments.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onChangePage={handleChangePage}
        onChangeRowsPerPage={handleChangeRowsPerPage}
      />


        </div>

    )
}

export default ViewAssignment;
