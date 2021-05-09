import {useState,useEffect} from 'react'
import Paper from '@material-ui/core/Paper';
import {
  Chart,
  BarSeries,
  Title,
  ArgumentAxis,
  ValueAxis,
} from '@devexpress/dx-react-chart-material-ui';
//import CustomPieChart from "../CustomPieChart";
import { PieChart } from 'react-minimal-pie-chart';
import {CanvasJSChart} from 'canvasjs-react-charts'

import { Animation } from '@devexpress/dx-react-chart';
const Dashboard = () => {
  const token = sessionStorage.getItem('token');
  console.log("token = view()", token);
  const data = [
    { year: '1950', population: 2.525 },
    { year: '1960', population: 3.018 },
    { year: '1970', population: 3.682 },
    { year: '1980', population: 4.440 },
    { year: '1990', population: 5.310 },
    { year: '2000', population: 6.127 },
    { year: '2010', population: 6.930 },
  ];
  const [showForm, setShowForm] = useState(false)
  const [showForm2, setShowForm2] = useState(false);
  const [indv, setinv] = useState([])
  const [grps, setgrps] = useState([])
  const [email, setemail] = useState('')
  const [tutGrp, settutGrp] = useState('')
  const fetchAssignmentDetails = async (url)=> {
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
const [bar1, setbar1] = useState([])
const [bar2, setbar2] = useState([])
const onSubmit2 = (e)=>{
  
    
      e.preventDefault()
      const getgrpstats = async()=>{
    const studentsFromServer = await fetchAssignmentDetails(`http://localhost:5000/teacher/statistics/group/${tutGrp}`)
    setgrps(studentsFromServer)
    setdetails(studentsFromServer.attemptedDifficulties)
    setbar1(studentsFromServer.scoresAchieved.universeTotal)
    setbar2(studentsFromServer.percentageCompletion)
    setline(studentsFromServer.scoreHistory)
  }
  getgrpstats()
      
  settutGrp('')
  }
  
  const onSubmit =  (e)=>{
      e.preventDefault()
      const getindstats = async()=>{
    const studentsFromServer = await fetchAssignmentDetails(`http://localhost:5000/teacher/statistics/${email}`)
    setinv(studentsFromServer)
    setdetails(studentsFromServer.attemptedDifficulties)
    setbar1(studentsFromServer.scoresAchieved.universeTotal)
    setbar2(studentsFromServer.percentageCompletion.barGraph)
    setline(studentsFromServer.scoreHistory)
  }
  getindstats()
      
      setemail('')
  }
  console.log(indv)
  console.log(grps)
 
  const Form = () => {
    setShowForm(!showForm)
  }
  const Form2 = () => {
    setShowForm2(!showForm2)
  }
  const [details, setdetails] = useState([
    { id: 'avgEasyCorrect', title: 'Easy',value: 1,color: '#ff9800'},
    { id: 'avgHardCorrect', title: 'Hard', value: 55,color: '#4caf50'},
    { id: 'avgMediumCorrect', title: 'Medium',value: 40,color: '#00acc1' }
    ])
    const [line, setline] = useState([
      { x: 1, y: 64 },
      { x: 2, y: 61 },
      { x: 3, y: 64 },
      { x: 4, y: 62 },
      { x: 5, y: 64 },
      { x: 6, y: 60 },
      { x: 7, y: 58 },
      { x: 8, y: 59 },
      { x: 9, y: 53 },
      { x: 10, y: 54 },
      { x: 11, y: 61 },
      { x: 12, y: 60 },
      { x: 13, y: 55 },
      { x: 14, y: 60 },
      { x: 15, y: 56 },
      { x: 16, y: 60 },
      { x: 17, y: 59.5 },
      { x: 18, y: 63 },
      { x: 19, y: 58 },
      { x: 20, y: 54 },
      { x: 21, y: 59 },
      { x: 22, y: 64 },
      { x: 23, y: 59 }
    ])
  const options = {
    animationEnabled: true,
    exportEnabled: true,
    theme: "light1", // "light1", "dark1", "dark2"
    title:{
      text: "Scores earned in last 14 days"
    },
    axisY: {
      title: "Score",
    },
    axisX: {
      title: "Day",
      prefix: "D",
      interval: 1
    },
    data: [{
      type: "line",
      toolTipContent: "Week {x}: {y}",
      dataPoints: line
    }]
  }
console.log(bar2);
  return (
    <div className = "center">
    <div className = 'form-control' >
      <input  type = 'submit' value = 'Statistics by Tutorial Group'className ='btn2 btn2-block-stats' onClick={Form2}/>
      <input type = 'submit' value = 'Statistics of Individual Student' className ='btn2 btn2-block-stats' onClick={Form}/>
      
      {showForm && (
        
        <form className = 'form' onSubmit={onSubmit}>
          <label className = 'field3'>Email Address:</label>
                <input type='text' placeholder = 'Enter Ntu Email address' className = 'field2' value={email}required onChange={(e)=> setemail(e.target.value)}/>         
                <input type = 'submit' value = 'enter' className ='btn2 btn2-block-stats' />
        </form>)}
        {showForm2 && (
        
        <form className = 'form' onSubmit={onSubmit2}>
          <label className = 'field3'>Tutorial Group:</label>
                <input type='text' placeholder = 'Enter Tutorial Group' className = 'field2' value={tutGrp}required onChange={(e)=> settutGrp(e.target.value)}/>         
                <input type = 'submit' value = 'Enter' className ='btn2 btn2-block-stats' />
        </form>)}
     </div>
     <div className="bar1">
        <Chart
          data={bar1}
        >
          <ArgumentAxis />
          <ValueAxis max={7} />
          <BarSeries
            valueField="totalScore"
            argumentField="identifier"
          />
          <Title text="Scores Achieved in each Universe" />
          <Animation />
        </Chart>
        </div>
        
        <div className='bar2'>
        <Chart
          data={bar2}
        >
          <ArgumentAxis />
          <ValueAxis max={7} />

          <BarSeries
            valueField="percentage"
            argumentField="universe"
          />
          <Title text="Percentage Completed in each Universe" />
          <Animation />
        </Chart>
      </div>
      <div className='pie'>
        <PieChart 
            radius={PieChart.defaultProps.radius-10}
              viewBoxSize={[100,100]}
              data={details}
              segmentsShift={(index) => (index === 0 ? 1 : 0.5)}
              label={({ dataEntry }) => "Avg "+dataEntry.title +":  "+dataEntry.value}
              labelStyle={{
                fontSize: '3px',
                labelPosition: 30
              }}
            />
          
          </div>
            <div className='line'>
              <CanvasJSChart options = {options}/>
              </div>

              {/* <div className='pie'>
      <CustomPieChart stat={details} main={true}/>
      </div> */}
    </div>
  )
}

export default Dashboard
