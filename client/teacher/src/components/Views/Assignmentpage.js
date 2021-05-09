import React from 'react'
import Paper from '@material-ui/core/Paper'
import {useState,useEffect} from 'react'
import {
      Chart,
      Title,
      ArgumentAxis,
      ValueAxis,
      Tooltip
    } from '@devexpress/dx-react-chart-material-ui'
import { Animation } from '@devexpress/dx-react-chart'
import { Histogram, DensitySeries, BarSeries, withParentSize, XAxis, YAxis } from '@data-ui/histogram';
import { withStyles} from '@material-ui/core/styles'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TablePagination from '@material-ui/core/TablePagination'
import TableRow from '@material-ui/core/TableRow'


import FilterListIcon from '@material-ui/icons/FilterList';
import IconButton from '@material-ui/core/IconButton';
import Particles from 'react-particles-js';



const Assignmentpage = (props) => {
  const ResponsiveHistogram = withParentSize(({ parentWidth, parentHeight, ...rest}) => (

    <Histogram
      width={parentWidth}
      height={600}
       {...rest}
    />
  ));
  const {id} = props.match.params
const assID = parseInt(id, 10)
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
  const [rows, setrows] = useState([])
    const [details, setdetails] = useState({scores:[0]})
    const columns = [
      { id: 'name', label: 'Student Name', minWidth: 170, type: 'link' },
      { id: 'scores', label: 'Total Score', minWidth: 100 },{ id: 'attemptStatus', label: 'Status', minWidth: 100 }
      ]

      const fetchAssignmentDetails = async (id)=> {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer "+token);
        const res = await fetch(`http://localhost:5000/teacher/assignment/${assID}`,{
          method: 'GET',
          headers: myHeaders,
          redirect: 'follow'
        })
        const data = await res.json()
        const temp = data;
        return temp
        
      };
      let scoresFromServer;
      useEffect(()=>{
        const getscores = async()=>{
          scoresFromServer = await fetchAssignmentDetails()
          setrows(scoresFromServer.students)
          setdetails(scoresFromServer);
          }
        getscores()
      },[setrows], [setdetails]);
    return (
        <div >
          <div className='paper'>
        <ResponsiveHistogram 
        ariaLabel="My histogram of ..."
        orientation="vertical"
        cumulative={false}
        normalized={false}
        binCount={10}
        valueAccessor={datum => datum}
        binType="numeric"
        renderTooltip={({ event, datum, data, color }) => (
          <div>
            <strong style={{ color }}>{datum.bin0} to {datum.bin1}</strong>
            <div><strong>count </strong>{datum.count}</div>
            <div><strong>cumulative </strong>{datum.cumulative}</div>
            <div><strong>density </strong>{datum.density}</div>
          </div>
        )}
      >
        <BarSeries
          rawData={details.scores}
        />
        <XAxis label="Assignment Score"/>
        <YAxis label="Count of Students"/>
      </ResponsiveHistogram>
      </div>
        <div className="score">
        <h3>Max Score : {details.maxScore} Min Score : {details.minScore} </h3>
        <h3>Mean : {details.avgScore}</h3>
        </div>
        <TableContainer className ='table-container'> 
        <Table stickyHeader aria-label="sticky table">
        <TableHead>
                        <TableRow>{columns.map((column)=>{
                          
                          return(<StyledTableCell key = {column.id} align ={column.align} style={{minWidth: column.minWidth}} >{column.label}</StyledTableCell>)})}
                          </TableRow>
                    </TableHead>
                    <TableBody>
                      
                    {rows.map((row, index) => {
                      
              return (
                <TableRow hover role="checkbox" tabIndex={-1} key={row.id}>
                  {columns.map((column) => {
                    const value = row[column.id]; 
                    return (
                      <StyledTableCell key={column.id} align={column.align}>{value}</StyledTableCell>
                    );
                  })}
                </TableRow>
              );
            })}
                    </TableBody>
                    </Table>
        </TableContainer>
        </div>

            
    )
          }

export default Assignmentpage
