// import React, { useCallback } from 'react'
// import { createMuiTheme, withStyles, makeStyles, ThemeProvider  } from '@material-ui/core/styles'
// import Card from '@material-ui/core/Card'
// import CardActionArea from '@material-ui/core/CardActionArea'
// import CardActions from '@material-ui/core/CardActions'
// import CardContent from '@material-ui/core/CardContent'
// import CardMedia from '@material-ui/core/CardMedia'
// import Button from '@material-ui/core/Button'
// import Typography from '@material-ui/core/Typography'
// import {useState,useEffect} from 'react'
// import AddTwoToneIcon from '@material-ui/icons/AddTwoTone'
// import IconButton from '@material-ui/core/IconButton'
// import TablePagination from '@material-ui/core/TablePagination'
// import Assignmentpage from '../Views/Assignmentpage'
// import Link from '@material-ui/core/Link'
// import Rough from './Rough'
// import TextField from '@material-ui/core/TextField';
// import Dialog from '@material-ui/core/Dialog';
// import DialogActions from '@material-ui/core/DialogActions';
// import DialogContent from '@material-ui/core/DialogContent';
// import DialogTitle from '@material-ui/core/DialogTitle';
// import Checkbox from '@material-ui/core/Checkbox';
// import { Alert, View, StyleSheet, Linking } from 'react-native';
// import AssignQues from './AssignQues'
// import {produce} from 'immer';
// import { Alert, View, StyleSheet, Linking, Text, TouchableOpacity } from 'react-native';
// import { TwitterIcon, RedditIcon } from 'react-share';

// const ViewAssignments = () => {
//     const [assignments, setAssignments] = useState([])
//     const [AddOpen, setAddOpen] = React.useState(false);
//     const [fullDataQuestions, setFullData] = React.useState({});
//     const [inputState, setInput] = React.useState({});
//     const [optionA,setA] = useState('');
//     const [optionB,setB] = useState('');
//     const [optionC,setC] = useState('');
//     const [optionD,setD] = useState('');
//     const [arr, setArr] = useState([0,1,2]);
//     const [updated, setUpdate] = useState(true);
//     const [page, setPage] = useState(0);
//     const [rowsPerPage, setRowsPerPage] = useState(3);
//     const [loading, setLoading] = useState(false);
//     const [question,setQuestion] = useState({});
//     const [questions, setQuestions] = useState([
//       {
    
//       points: 0,
//       questionID: 0,
//       body: "",
//       correctOption: "",
//       wrongOptions:[]}
//   ]);
//     const [questionCount, setQuestionCount]=useState([1])
//     const [prevCount,setprevCount]=useState([]);
//     // const [correct,setCorrect] = useState('');
//     const [currentQID, setCurrentQID]=useState(-1);
//     const [plus, setPlus] = useState(false);
//     function handlenewInput(){
//       setPlus(true);
//     }
//     const getOptionFromIndex = (val) => {
//       switch (val) {
//         case "0": return optionA;
//         case "1": return optionB;
//         case "2": return optionC;
//         case "3": return optionD;
//       }
//       return "";
//     }
//     const addQuestion=()=>{
//       let tempArr=questionCount;
//       setprevCount(questionCount);
//       console.log("Hello");
//       console.log(prevCount);
//       tempArr.push(1);
//       console.log("Wow");
//       console.log(tempArr);
//       //setprevCount(tempArr);
//       setQuestionCount(tempArr);
      
//       console.log(questionCount);
//     }

//     const handleChangeQuestion = (event) => {
//       console.log('target val',event.target.value);
//         switch(event.target.name){
//           // case "":
//           //   setA(event.target.value);
//           //   break;
//           case "wrongOptions[0]":
//             setB(event.target.value);
//             break;
//           case "wrongOptions[1]":
//             setC(event.target.value);
//             break;
//           case "wrongOptions[2]":
//             setD(event.target.value);
//             break;
//           default:
//             if (event.target.name == "questionID")
//               {question[event.target.name] = Number(event.target.value);}
//             else
//               {
//                 question[event.target.name] = (event.target.value);
//               }
//             }
//         //     if (event.target.name == "body" || event.target.name == "correctOption" )
//         //       {question[event.target.name] = event.target.value;}
//         //     else
//         //       {question[event.target.name] = Number(event.target.value);}
//         //     break;
//         // }
//         // if (event.target.name == "questionID")
//         //   {
//         //     inputState.questionIDs.push(event.target.value);
//         //     setInput(inputState);
//         //   }
//         console.log(inputState)
//         // if (event.target.name == wrongOptions)
        
//         setQuestion(question);
        

//       };
//     function handleAddQuestionok()
//     {
//       question.wrongOptions.push(optionB);
//       question.wrongOptions.push(optionC);
//       question.wrongOptions.push(optionD);
//       inputState[0].questionIDs.push(question.questionID);
//       inputState.push(question);  
//       console.log('add quest enter');
//       inputState.push(question);
//       setInput(inputState);

//     }
              

//     const fetchAssignments = async ()=> {
//       var myHeaders = new Headers();
//       myHeaders.append("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IlNVMDAxQGUubnR1LmVkdS5zZyIsImlkIjoiNjA1MzE2Njk5ZDRhNjI0MmYwZDk5M2RmIiwidHV0R3AiOiJTQ0U0IiwiaWF0IjoxNjE2MDU4MTg2fQ.7LFzy-ecqB89ZNydkPR0LhuM33SV3ciaPJmO_g9oQnc");
//       const res = await fetch('http://localhost:5000/teacher/assignment/list/SCE5',{
//         method: 'GET',
//         headers: myHeaders,
//         redirect: 'follow'
//       })
//       const data = await res.json()
//       console.log(data);
//       return data
//     }
//     useEffect(()=>{
//       const getAssignments = async()=>{
//         const assignementsFromServer = await fetchAssignments()
        
//         setAssignments(assignementsFromServer)
  
//       }
//       getAssignments()
//     },[setAssignments])
//     const ColorButton = withStyles((theme) => ({
//         root: {
//             body :{
//                 color: 'white'
//               },
//             backgroundColor: '#6f7bd9',
//             margin:'auto',
//           marginRight:'2rem'
//         }
//       }))(Button); 
//       const IColourButton = withStyles((theme) => ({
//         root: {
//             body :{
//                 color: 'white'
//               },
//             backgroundColor: '#6f7bd9',
            
//           marginLeft:'49rem'
//         }
//       }))(IconButton);  


//   const handleChangePage = (e, newPage) => {
//     setPage(newPage);
//   };

//   const handleChangeRowsPerPage = (e) => {
//     setRowsPerPage(+e.target.value);
//     setPage(0);
//   }; 

//   const toggleAddModal = () => {
//     const newObj = [
//       {
//         assignmentName:"",
//         timeLimit:1,
//         questionIDs:[],
//         deadline:"",
//         tutGrp:""
//       }
//     ]
//     const newQues = {
//       questionID: 1,
//       body:"",
//       correctOption: "0",
//       wrongOptions:[],
//       points:1
//     }
      
//     setAddOpen(true);
//     setInput(newObj);
//     setQuestion(newQues);
    
//   };

//   const checkAns = (event) => {
//     console.log('check ans entered');
//     const isChecked = event.target.checked;
//     console.log(isChecked)
//     console.log('name',event.target.name)
//     if(isChecked){
//       setQuestion({...question, 
//         correctOption: getOptionFromIndex(event.target.name), 
//         wrongOptions: ["0", "1", "2", "3"].filter(val => {
//           return val !== event.target.name;
//         }).map(getOptionFromIndex)
//       });
//       // setCorrect(event.target.name);
//     }
//     else {
//       setQuestion({...question, 
//         correctOption: "", 
//         wrongOptions: ["0", "1", "2", "3"].map(getOptionFromIndex)
//       });
//     }
//     console.log('ques',question);
    
//   }

//   const handleChange = (event) => {
//     console.log('target val',event.target.value);
//     console.log('A',optionA,'B',optionB,'C',optionC,'D',optionD);
//     if (event.target.name == "assignmentName" || event.target.name == "tutGrp" || event.target.name == "deadline"  )
//             {inputState[0][event.target.name] = event.target.value;}
//           else
//             {inputState[0][event.target.name] = Number(event.target.value);}
//     setInput(inputState);
//       // switch(event.target.name){
//       //   case "OptionA":
//       //     setA(event.target.value);
//       //     break;
//       //   case "OptionB":
//       //     setB(event.target.value);
//       //     break;
//       //   case "OptionC":
//       //     setC(event.target.value);
//       //     break;
//       //   case "OptionD":
//       //     setD(event.target.value);
//       //     break;
//         // default:
//           // if (event.target.name == "assignmentName" || event.target.name == "tutG" )
//           //   {inputState[event.target.name] = event.target.value;}
//           // else
//           //   {inputState[event.target.name] = Number(event.target.value);}
//           // break;
//       // }
      
      
//     };

//   const handleAddOk = () => {
//     console.log('Input',inputState);
//     handleAddQuestionok();
//     setInput(inputState);
//     //TODO: Update DB question 
//     var myHeaders = new Headers();
//     myHeaders.append("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IlNVMDAxQGUubnR1LmVkdS5zZyIsImlkIjoiNjA1MzE2Njk5ZDRhNjI0MmYwZDk5M2RmIiwidHV0R3AiOiJTQ0U0IiwiaWF0IjoxNjE2MDU4MTg2fQ.7LFzy-ecqB89ZNydkPR0LhuM33SV3ciaPJmO_g9oQnc");
//     myHeaders.append("Content-Type", "application/json");
    
//     var raw = JSON.stringify(inputState);
    
//     var requestOptions = {
//       method: 'POST',
//       headers: myHeaders,
//       body: raw,
//       redirect: 'follow'
//     };
    
//     fetch("http://localhost:5000/teacher/assignment/"+String(inputState[0].assignmentID), requestOptions)
//       .then(response => response.text())
//       .then(result => console.log(result))
//       .catch(error => console.log('error', error));

//     setUpdate(true);
//     setAddOpen(false);
//   };

//   const handleAddCancel = () => {
//       setAddOpen(false);
//   };

//   const [counter,setCounter] = useState([1,2,3,4]);

// // async function onShare(){
// //   await Linking.openURL("https://twitter.com/intent/tweet?text=I+have+shared+assignment+1.+Solve+by+today+.+&amp;lang=en");
// //   await Linking.openURL("https://www.reddit.com/submit?text=I%20scored%200%20in%20challenge%20Challenge%201.%20Try%20and%20beat%20my%20score.%20&title=Softvengers%20challenge!");
// // }

// String.format = function() {
//   var theString = arguments[0];
//   for (var i = 1; i < arguments.length; i++) {
//       var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
//       theString = theString.replace(regEx, arguments[i]);
//   }
//   return theString;
// }

// function tweetNow(assignment){
//   let twitterParameters = [];
//   let tweetContent = String.format("Shared Assignment {0}\nName: {1}\nDuration: {2}\nDeadline: {3}", assignment.assignmentID, assignment.assignmentName, assignment.timeLimit, assignment.deadline);
//   twitterParameters.push('text=' + encodeURI(tweetContent));
//   //twitterParameters.push('url=' + encodeURI(twitterShareURL));  
//   //twitterParameters.push('via=' + encodeURI(twitterViaAccount));
//   const url =
//     'https://twitter.com/intent/tweet?'
//     + twitterParameters.join('&');
//   Linking.openURL(url);
// };

// function redditNow(assignment){
//   let redditParameters = [];
//   let redditContent = String.format("Shared Assignment {0}\nName: {1}\nDuration: {2}\nDeadline: {3}", assignment.assignmentID, assignment.assignmentName, assignment.timeLimit, assignment.deadline);
//   redditParameters.push('text=' + encodeURI(redditContent));
//   redditParameters.push('title=' + encodeURI("Softvengers assignment"));
//   const url =
//     'http://reddit.com/submit?'
//     + redditParameters.join('&');
//   Linking.openURL(url);
// };

//     return (
//         <div className='assignment-container'>
//             <h1>Assignments <IColourButton><AddTwoToneIcon onClick={toggleAddModal}/> </IColourButton></h1>
//             {assignments.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((assignment)=>(
//             <Card>
//                 <CardActionArea>
//                     <CardMedia title="Assignment"/>
//                     <CardContent><Typography gutterBottom variant="h5" component="h2">Assignment {assignment.assignmentID}</Typography>
//                     <Typography>{assignment.assignmentName}</Typography>
//                     <Typography>Due : {assignment.deadline}</Typography>
//                     <Typography>Timelimit: {assignment.timeLimit}</Typography>
//                     </CardContent>
//                 </CardActionArea>
//                 <CardActions>
//                     {/* <ColorButton size="large" variant="outlined" onClick={() => tweetNow(assignment)}>
//                     Share on Twitter</ColorButton>
//                      <Button imageUrl="https://w7.pngwing.com/pngs/529/867/png-transparent-computer-icons-logo-twitter-miscellaneous-blue-logo-thumbnail.png" onClick={() => tweetNow(assignment)} ></Button> */}
//                     <ColorButton onClick={() => tweetNow(assignment)} > 
//                     <TwitterIcon size={32} round={true} /> Share on Twitter</ColorButton>
//                     <ColorButton onClick={() => redditNow(assignment)}> 
//                     <RedditIcon size={32} round={true} /> Share on Reddit</ColorButton>
//                     <ColorButton size="large" variant="outlined" ><Link href="/Assignmentpage" >
//                     Statistics</Link></ColorButton>
//                 </CardActions>
//             </Card>))}
//             {/* <Button color="primary" round >Add new assignment</Button> */}
//             <Dialog open={AddOpen} onClose={handleAddCancel} aria-labelledby="form-dialog-title" maxWidth='xl'>
//                 <DialogTitle id="form-dialog-title" color='primary'>Add Assignment</DialogTitle>
//                     <DialogContent>
//                     <form  noValidate autoComplete="off">
                          
//                             <TextField name = "assignmentID" type="number" id="standard-basic" label="Assignment ID" required="true" style = {{width: '45%'}} onChange={handleChange}/>
//                             <TextField name = "assignmentName" id="standard-basic" label="Assignment Name" required="true" style = {{width: '45%'}} onChange={handleChange}/>
//                             <TextField name = "timeLimit" type="number" id="standard-basic" label="Time limit" required="true" style = {{width: '45%'}} onChange={handleChange}/>
//                             <TextField name = "deadline" type="date" id="standard-basic" label="Deadline" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChange}/>
//                             <TextField name = "tutGrp"  id="standard-basic" label="Tutorial Group" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChange}/>
                            
//                           </form>
//                           <div>
//                           <AssignQues handleChangeQuestion = {handleChangeQuestion} />
//                             {
//                             // questionCount.map(i => {
//                             //   return(
//                             //     <AssignQues handleChangeQuestion = {handleChangeQuestion}/>
//                             //   )
//                             // })
                            
//                             plus? <AssignQues handleChangeQuestion = {handleChangeQuestion}/>:<div/>
    
//                             }
//       </div>
//                     </DialogContent>
//                           <DialogActions>
//                             <Button onClick = {addQuestion}> Add Question </Button>
//                             <Button onClick={handleAddCancel} color="primary">
//                               Cancel
//                             </Button>
//                             <Button onClick={handleAddOk} color="primary">
//                               Add Assignment
//                             </Button>
//                             <Button onClick = {()=> {
//                         setQuestions(currQuestions=> [
//                             ...currQuestions,
//                             {
                                
//                                 wrongOptions: [],
//                             points: 0,
//                             questionID: 0,
//                             body: "",
//                             correctOption: ""
//                             }
//                         ])
//                     }}> Add New Assignment </Button>
//                     {questions.map((q,index) =>{
//                         return(
//                             <div key = {q.questionID}>
//                               {
//                                 arr.map( (i) => {
                               
//                                   <TextField onChange = {(e)=>{
//                                    const tempOption = e.target.value;
//                                    setQuestions(currQues => 
//                                        produce(currQues,(v)=>{
//                                                v[index].wrongOptions[i] = tempOption;
//                                                                })
//                                        ); 
   
//                                  }}
//                                  placeholder = "wrong options"/>
//                                })
//                               }
                            
                                
//                                 <TextField onChange = {(e)=>{
//                                     const points = e.target.value;
//                                     setQuestions(currPoint => 
//                                         produce(currPoint,(v)=>{
//                                             v[index].points = Number(points);
//                                         })
//                                         );
//                                 }}
//                                 placeholder = "Points"/>
                                
                               
//                                 <TextField placeholder = "question ID"/>
//                                 <TextField placeholder = "question body"/>
//                                 <TextField placeholder = "correct option"/>
//                             </div>
//                         )
//                     })}
//                         </DialogActions>
//             </Dialog>

//             <TablePagination
//         rowsPerPageOptions={[3,5,10,25,100]}
//         component="div"
//         count={assignments.length}
//         rowsPerPage={rowsPerPage}
//         page={page}
//         onChangePage={handleChangePage}
//         onChangeRowsPerPage={handleChangeRowsPerPage}
//       />
//       </div>
//     )
// }

// export default ViewAssignments
