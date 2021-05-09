//unused component 
import React, {useState, useRef, useEffect} from 'react';
import axios from 'axios';
import { makeStyles } from '@material-ui/core/styles';
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
import {Button} from 'reactstrap';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import Checkbox from '@material-ui/core/Checkbox';
import { dictOfUniverse, dictOfSolar, dictOfPlanet } from './../../variables/general';

const columns = [
    { id: 'QuestionID', label: 'Question ID', minWidth: 100},
    { id: 'Universe', label: 'Universe', minWidth: 100 },
    { id: 'Solar', label: 'Solar', minWidth: 100 },
    { id: 'planet', label: 'Planet', minWidth: 100 },
    { id: 'question', label: 'Question', minWidth: 100 }
   
  ];

  function createData(universe,solar, planet, questionID, question) {
    return { Universe: dictOfUniverse[universe-1], Solar: dictOfSolar[solar-1], Planet: dictOfPlanet[planet-1], QuestionID: questionID, question};
  }

  const useStyles = makeStyles((theme) => ({
    root: {
      '& .MuiButton-contained':{
        color: 'white',
        background: '#174A84'
      },
      '& > *': {
        margin: theme.spacing(1), 
      }
    },
    paperWrapper: {
      marginTop: '1em',
      width: '100%'
    },
    container: {
      maxHeight: 440,
    },
    searchBar: {
      padding: '2px 4px',
      display: 'flex',
      alignItems: 'center',
      width: 100,
      flex: 1,
      marginLeft: '30em'
    },
    iconButton: {
        padding: 10,
    },
    input: {
        marginLeft: theme.spacing(1),
        flex: 1,
    },
    bar: {
        display: 'flex',
        marginTop: '1em'
    },
    typo: {
      color: "#999"
    },
    cell: {
      color: "#3C4858"
    },
    check: {
      alignItems: 'left',
      flex:5
    },
  }));

  axios.defaults.baseURL = "http://localhost:5000/teacher";
const NewQuestion = () => {

    const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);
  const [question, setQuestion] = React.useState({});
    const [inputState, setInput] = React.useState({});
    const [isModalOpen, setIsModalOpen] = useState(false);
    // Testing of data retrieval 

    const [appState, setAppState] = useState({
      loading: false,
      allQuestions: null,
    });
  
    const [updated, setUpdate] = useState(true);
    const config = {
        headers: {Authorization: "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IlNVMDAxQGUubnR1LmVkdS5zZyIsImlkIjoiNjA1MzE2Njk5ZDRhNjI0MmYwZDk5M2RmIiwidHV0R3AiOiJTQ0U0IiwiaWF0IjoxNjE2MDU4MTg2fQ.7LFzy-ecqB89ZNydkPR0LhuM33SV3ciaPJmO_g9oQnc"}
    };
    useEffect(() => {
      setAppState({ loading: true });
      axios.get('/question',config).then((allQuestions) => {
        const allData = allQuestions.data.questions;
        setAppState({ loading: false, allQuestions: allData });
        console.log(allQuestions)
      });
      setUpdate(false);
    // var myHeaders = new Headers();
    //             myHeaders.append("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IlNVMDAxQGUubnR1LmVkdS5zZyIsImlkIjoiNjA1MzE2Njk5ZDRhNjI0MmYwZDk5M2RmIiwidHV0R3AiOiJTQ0U0IiwiaWF0IjoxNjE2MDU4MTg2fQ.7LFzy-ecqB89ZNydkPR0LhuM33SV3ciaPJmO_g9oQnc");
    //             fetch('http://localhost:5000/teacher/question',{headers: myHeaders})
    //             .then(response => response.json())
    //             .then(data => setAppState({loading: false, allQuestions: data}));
                
    // 
  }, [setAppState, updated]);
    
    const rows = [];
    appState.allQuestions && appState.allQuestions.forEach(question => rows.push(createData(question.Universe, question.Solar, question.Planet, question.QuestionID, question.body)));
    console.log('appState', appState);
    const classes = useStyles();


    
    const handleChange = (event) => {
      switch(event.target.name){
        case "OptionA":
          inputState.Options.A = event.target.value;
          break;
        case "OptionB":
          inputState.Options.B = event.target.value;
          break;
        case "OptionC":
          inputState.Options.C = event.target.value;
          break;
        case "OptionD":
          inputState.Options.D = event.target.value;
          break;
        default:
          inputState[event.target.name] = event.target.value;
          break;
      }
      setInput(inputState);
    };
  
    const handleChangePage = (event, newPage) => {
      setPage(newPage);
    };
  
    const handleChangeRowsPerPage = (event) => {
      setRowsPerPage(+event.target.value);
      setPage(0);
    };
  
    // Add dialog
    const [AddOpen, setAddOpen] = React.useState(false);
  
    const toggleAddModal = () => {
      const newObj = {
        Universe: 1,
        Solar: 1,
        Planet: 1,
        QuestionID: 1,
        Question: "",
        Type: "MCQ",
        Options: {
          "A": "",
          "B": "",
          "C": "",
          "D": ""
        },
        CorrectAns: "A",
      }
      setInput(newObj);
      setAddOpen(true);
    };
    // const config = {
    //     headers: { Authorization: `Bearer ${token}` }
    // };

    const handleAddOk = () => {
      //TODO: Update DB question 
    axios.post(`/question/${inputState.QuestionID}`, inputState, config)
    .then(res => console.log(res))
    .catch(err => console.log(err));
    setUpdate(true);
    setAddOpen(false);
    };
  
    const handleAddCancel = () => {
        setAddOpen(false);
    };
    // Edit dialog
    const [EditOpen, setEditOpen] = React.useState(false);
  
    const toggleEditModal = (index) => {
      const allQuestions = appState.allQuestions;
      setInput(allQuestions[index]);
  
      setEditOpen(true);
    };
  
    const [DeleteOpen, setDeleteOpen] = React.useState(false);
  
    const toggleDeleteModal = (index) => {
      const allQuestions = appState.allQuestions;
      setInput(allQuestions[index]);
  
      setDeleteOpen(true);
    }
  
    const handleDeleteCancel = () => {
      setDeleteOpen(false);
    }
  
    const handleDeleteOk = (index) => {
      axios.delete(`/question/${inputState.QuestionID}`)
      .then(res => console.log(res))
      .catch(err => console.log(err));
      setDeleteOpen(false);
      setUpdate(true);
    }
  
    const handleEditOk = () => {
      axios.patch(`/question/${inputState.QuestionID}`, inputState)
      .then(res => console.log(res))
      .catch(err => console.log(err));
      setEditOpen(false);
      setUpdate(true);
    };
  
    const handleEditCancel = () => {
        setEditOpen(false);
    };
  
    const checkAns = (event) => {
      const isChecked = event.target.checked;
      if(isChecked){
        inputState.CorrectAns = event.target.name;
      }
    }
  
    return (
      <div className={classes.root}>
        <Typography variant="h4" gutterBottom className={classes.typo}>
            Question Bank
        </Typography>
        <div className={classes.bar}>
            <Button color="primary" round onClick={toggleAddModal}>Add new question</Button>
            <Dialog open={AddOpen} onClose={handleAddCancel} aria-labelledby="form-dialog-title" maxWidth='xl'>
                          <DialogTitle id="form-dialog-title" color='primary'>Add Question</DialogTitle>
                          <DialogContent>
                          <form className={classes.root} noValidate autoComplete="off">
                            <TextField name = "Universe" type="number" id="standard-basic" label="Universe" required="true" style = {{width: '45%'}} onChange={handleChange}/>
                            <TextField name = "Universe" type="number" id="standard-basic" label="Solar" required="true" style = {{width: '45%'}} onChange={handleChange}/>
                            <TextField name = "Planet" type="number" id="standard-basic" label="Planet" required="true" style = {{width: '45%'}} onChange={handleChange}/>
                            <TextField name = "questionID" type="number" id="standard-basic" label="QuestionID" required="true" style = {{width: '45%'}} onChange={handleChange}/>
                            <TextField name = "Question" id="standard-basic" label="Quiz Question" fullWidth="true" required="true" style = {{width: '91%'}} onChange={handleChange}/>
                            <Typography variant="h7" >
                            <br></br> {"     "}Select the Checkbox with the correct option:
                            </Typography>
                            <div className={classes.check} >
                            <Checkbox
                              defaultChecked
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="A"
                              onChange={checkAns}
                            />
                            <TextField id="standard-basic" label="Option 1" required="true" style = {{width: '90%'}} name = "OptionA" onChange={handleChange}/>
                            </div>
                            <div className={classes.check} >
                            <Checkbox
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="B"
                              onChange={checkAns}
                            />
                            <TextField id="standard-basic" label="Option 2" required="true" style = {{width: '90%'}} name = "OptionB" onChange={handleChange}/>
                            </div>
                            <div className={classes.check} >
                            <Checkbox
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="C"
                              onChange={checkAns}
                            />
                            <TextField id="standard-basic" label="Option 3" required="true" style = {{width: '90%'}} name = "OptionC" onChange={handleChange}/>
                            </div>
                            <div className={classes.check} >
                            <Checkbox
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="D"
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
            <Paper component="form" className={classes.searchBar}>
                <InputBase
                    className={classes.input}
                    placeholder="Search..."
                />
                <IconButton type="submit" className={classes.iconButton} aria-label="search">
                    <SearchIcon />
                </IconButton>
            </Paper>
        </div>
        <Paper className={classes.paperWrapper}>
          <TableContainer className={classes.container}>
            <Table>
              <TableHead>
                <TableRow>
                  {columns.map((column) => (
                    <TableCell
                      key={column.id}
                      align={column.align}
                      style={{ minWidth: column.minWidth }}
                    >
                      {column.label}
                    </TableCell>
                  ))}
                </TableRow>
              </TableHead>
              <TableBody>
                {rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row, index) => {
                  const realIndex = index + page * rowsPerPage;
                  return (
                    <TableRow hover role="checkbox" tabIndex={-1} key={row.code}>
                      {columns.map((column) => {
                        const value = row[column.id];
                        return (
                          <TableCell key={column.id} align={column.align} className={classes.cell}>
                            {column.format && typeof value === 'number' ? column.format(value) : value}
                          </TableCell>
                        );
                      })}
                      <IconButton onClick={() => toggleEditModal(realIndex)}>
                        <EditIcon />
                      </IconButton>
                      <Dialog open={EditOpen} onClose={handleEditCancel} aria-labelledby="form-dialog-title" maxWidth='xl'>
                          <DialogTitle id="form-dialog-title" color='primary'>Edit Question</DialogTitle>
                          <DialogContent>
                          <form className={classes.root} noValidate autoComplete="off">
                            <TextField name = "Universe" id="standard-basic" label="Universe" required="true" style = {{width: '45%'}} defaultValue={dictOfUniverse[inputState.Universe-1]} onChange={handleChange}/>
                            <TextField name = "Solar" id="standard-basic" label="Solar" required="true" style = {{width: '45%'}}  defaultValue={dictOfSolar[inputState.Solar-1]} onChange={handleChange}/>
                            <TextField name = "Planet" id="standard-basic" label="Planet" required="true" style = {{width: '45%'}} defaultValue={dictOfPlanet[inputState.Planet-1]} onChange={handleChange}/>
                            <TextField name = "Question" id="standard-basic" label="Quiz Question" fullWidth="true" required="true" style = {{width: '91%'}} defaultValue={inputState.Question} onChange={handleChange}/>
                          
                            <Typography variant="h7" >
                            <br></br> {"     "}Select the Checkbox with the correct option:
                            </Typography>
                            <div className={classes.check} >
  
                            <Checkbox
                              defaultChecked = {inputState.CorrectAns === "A"}
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="A"
                              onChange={checkAns}
                            />
                            <TextField name = "OptionA" id="standard-basic" label="Option 1" required="true" style = {{width: '90%'}} defaultValue={inputState.Options && inputState.Options.A} onChange={handleChange}/>
                            </div>
                            <div className={classes.check} >
                            <Checkbox
                              defaultChecked = {inputState.CorrectAns === "B"}
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="B"
                              onChange={checkAns}
                            />
                            <TextField name = "OptionB" id="standard-basic" label="Option 2" required="true" style = {{width: '90%'}} defaultValue={inputState.Options && inputState.Options.B} onChange={handleChange}/>
                            </div>
                            <div className={classes.check} >
                            <Checkbox
                              defaultChecked = {inputState.CorrectAns === "C"}
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="C"
                              onChange={checkAns}
                            />
                            <TextField name = "OptionC" id="standard-basic" label="Option 3" required="true" style = {{width: '90%'}} defaultValue={inputState.Options && inputState.Options.C} onChange={handleChange}/>
                            </div>
                            <div className={classes.check} >
                            <Checkbox
                              defaultChecked = {inputState.CorrectAns === "D"}
                              color="primary"
                              inputProps={{ 'aria-label': 'secondary checkbox' }}
                              name="D"
                              onChange={checkAns}
                            />
                            <TextField name = "OptionD" id="standard-basic" label="Option 4" required="true" style = {{width: '90%'}} defaultValue={inputState.Options && inputState.Options.D} onChange={handleChange}/>
                            </div>
                          </form>
                          </DialogContent>
                          <DialogActions>
                            <Button onClick={handleEditCancel} color="primary">
                              Cancel
                            </Button>
                            <Button onClick={handleEditOk} color="primary">
                              Update Quiz
                            </Button>
                          </DialogActions>
                        </Dialog>
                      <IconButton onClick={() => toggleDeleteModal(realIndex)}>
                        <DeleteIcon/>
                      </IconButton>
                      <Dialog open={DeleteOpen} onClose={handleDeleteCancel} aria-labelledby="form-dialog-title" maxWidth='xl'>
                        <DialogTitle id="form-dialog-title" color='primary'>Delete Question</DialogTitle>
                        <DialogContent>Are you sure you want to delete this question?</DialogContent>
                        <DialogActions>
                          <Button onClick={handleDeleteCancel} color="primary">
                            Cancel
                          </Button>
                          <Button onClick={handleDeleteOk} color="primary">
                            Delete
                          </Button>
                        </DialogActions>
                      </Dialog>
                    </TableRow>
                  );
                })}
              </TableBody>
            </Table>
          </TableContainer>
          <TablePagination
            rowsPerPageOptions={[5, 10, 15]}
            component="div"
            count={rows.length}
            rowsPerPage={rowsPerPage}
            page={page}
            onChangePage={handleChangePage}
            onChangeRowsPerPage={handleChangeRowsPerPage}
          />
        </Paper>
      </div>
    );
  }
  export default NewQuestion          
                     
