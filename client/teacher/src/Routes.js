import React, { useState } from 'react';
import Navbar from './components/Navbar/Navbar'
import CreateStudentAccount from './components/Views/CreateStudentAccount'
import ViewLeaderboard from './components/Views/ViewLeaderboard'

import Dashboard from './components/Views/Dashboard'
import Questionbank from './components/Views/ViewQuestionbank'
import CreateAssignment  from './components/Views/CreateAssignment'
import Assignmentpage  from './components/Views/Assignmentpage'

import { BrowserRouter,Route, Switch, Redirect } from 'react-router-dom'
import Login from './components/Views/LoginScreen';
import LoginScreen from './components/Views/LoginScreen';
import Particles from 'react-particles-js';
import ViewAssignment from './components/Views/ViewAssignment';
import useToken from'./variables/useToken';
import hello from './components/Views/hello'
import Logout from './components/Views/Logout'


const Routes = () => {
    //console.log(props.token);
    const setToken = useToken().setToken;
    const token = useToken().token;
    const [showNavbar, setShowNavbar] = useState(true);
    console.log("ROUTES"+token);
    return (
        
        <BrowserRouter>
        <div>

            
            {showNavbar? <Navbar token = {token}/> : <div/>}

            <Switch>
                <Route exact path= "/hello" component = {hello}/>
                <Route exact path="/Statistics" component={Dashboard} />
                <Route exact path="/Assignments" component={ViewAssignment} />
                {/* <Route path="/Assignmentpage" component={Assignmentpage}/> */}
                <Route path='/Assignmentpage/:id' exact component={Assignmentpage} />
                <Route exact path="/QuestionBank" component={Questionbank}/>
                <Route exact path="/Leaderboard" component={ViewLeaderboard}/>
                <Route exact path="/CreateStudentAccount" component={CreateStudentAccount}/>
                {/* <Route exact path ="/Logout" component = {Logout}/> */}
            </Switch>
            <Route exact path="/Logout" component={() => <Logout setShowNavbar = {setShowNavbar}/>} />
            
            
            
      </div>
        </BrowserRouter>
    )
    }


export default Routes
