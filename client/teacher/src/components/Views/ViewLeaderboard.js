import React from 'react'
import { withStyles} from '@material-ui/core/styles'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TablePagination from '@material-ui/core/TablePagination'
import TableRow from '@material-ui/core/TableRow'
import {useState,useEffect} from 'react'
import Tooltip from '@material-ui/core/Tooltip';
import FilterListIcon from '@material-ui/icons/FilterList';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import Particles from 'react-particles-js';
import IconButton from '@material-ui/core/IconButton';
const ViewLeaderboard = () => {
  const token = sessionStorage.getItem('token');
  console.log("token = view()", token);
  const StyledTableCell = withStyles((theme) => ({
    head: {
      backgroundColor:'#6f7bd9',
     color: theme.palette.common.black,
      fontSize: 24,
      
    },
    body: {
      fontSize: 20,
      color: theme.palette.common.black,
    },
  }))(TableCell);
  const ColorTableContainer= withStyles((theme) => ({
    body:{
      backgroundColor: '#ffffff',
    }
  }))(TableContainer); 
  
  const columns = [
        { id: 'rank', label: 'Rank', minWidth: 100},
        { id: 'name', label: 'Student Name', minWidth: 170, type: 'link' },
        { id: 'totalScore', label: 'Total Score', minWidth: 100 },{ id: 'tutGrp', label: 'Tutorial Group', minWidth: 100 }
        ]
  const [rows, setData] = useState([])
  const [tut, settut] = useState('')

  const fetchStudents = async (url)=> {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer "+token);
    const res = await fetch(url,{
      method: 'GET',
      headers: myHeaders,
      redirect: 'follow'
    })
    const data = await res.json()
    
    return data
  }
  

  useEffect(()=>{
    const getStudents = async()=>{
      const studentsFromServer = await fetchStudents('http://localhost:5000/teacher/leaderboard')
      const allStudents = studentsFromServer.sort((a, b) => parseFloat(b.totalScore) - parseFloat(a.totalScore));
      setData(allStudents)}
    getStudents()
  },[setData])
  console.log(rows);
  console.log(rows);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const handleChangePage = (e, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (e) => {
    setRowsPerPage(+e.target.value);
    setPage(0);
  };
  
  const filterByTutGrp=(e)=>{
    console.log(e);
    
    const getStudents = async()=>{
      const studentsFromServer = await fetchStudents(`http://localhost:5000/teacher/${tut}`)
      const allStudents = studentsFromServer.sort((a, b) => parseFloat(b.totalScore) - parseFloat(a.totalScore));
      setData(allStudents);
      
      
  
  }
  getStudents();

  
}
const handleChange = async (e) => {
  settut(e.target.value);
};
  
    return (
        <div className ='table-container'>
          <h1 className ='LeaderBoard'>LeaderBoard</h1>
          
            <TableContainer >
                <Table stickyHeader aria-label="sticky table">
                
                    <TableHead>
                        <TableRow>{columns.map((column)=>{
                          const value = column.id === 'tutGrp' ? <>
                          {column.label} <Tooltip title = 'Fiter By Tutorial Group'><IconButton onClick={filterByTutGrp}><FilterListIcon/>
                         </IconButton></Tooltip><Select
          labelId="demo-simple-select-label"
          id="demo-simple-select"
          value={tut}
          onClick={handleChange}
        >
          <MenuItem value={'leaderboard'} >None</MenuItem>
          <MenuItem value={'leaderboard/SCE4'} >SCE4</MenuItem>
          <MenuItem value={'leaderboard/SCE5'}>SCE5</MenuItem>
        </Select></>
                          : `${column.label}`;
                          return(<StyledTableCell key = {column.id} align ={column.align} style={{minWidth: column.minWidth}} >{value}</StyledTableCell>)})}
                      </TableRow>
                    </TableHead>
                    <TableBody>
                    {rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row, index) => {
              return (
                <TableRow hover role="checkbox" tabIndex={-1} key={row.id}>
                  {columns.map((column) => {
                    const value = column.id === 'rank' ? `Rank ${index + page * rowsPerPage + 1} ` :row[column.id] ;
                    const rank1 = (value.length===7&&value[5] === '1')? `Rank 1 🥇` : value;
                    const rank2 = (value.length===7&&value[5] === '2')? `Rank 2 🥈` : rank1;
                    const rank3 = (value.length===7&&value[5] === '3')? `Rank 3 🥉` : rank2;
                    return (
                      <StyledTableCell key={column.id} align={column.align}>{rank3}</StyledTableCell>
                    );
                  })}
                </TableRow>
              );
            })}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
        rowsPerPageOptions={[5,10, 25, 100]}
        component="div"
        count={rows.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onChangePage={handleChangePage}
        onChangeRowsPerPage={handleChangeRowsPerPage}
      />
      
        
        </div>
    )
}

export default ViewLeaderboard

