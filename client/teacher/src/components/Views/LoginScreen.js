import React, {Component} from 'react';
import {useState, useEffect} from 'react'
import Navbar from '../Navbar/Navbar';
import CreateStudentAccount from './CreateStudentAccount'
import ViewLeaderboard from './ViewLeaderboard'
import ViewAssignment from './ViewAssignment'
import Dashboard from './Dashboard'
import Questionbank from './ViewQuestionbank'
import Assignmentpage  from './Assignmentpage'
import { BrowserRouter,Route, Switch} from 'react-router-dom'
import Routes from '../../Routes';
import {Link, Redirect} from 'react-router-dom'
import PropTypes from 'prop-types';
import {withRouter} from 'react-router'
import logo from '../assets/imgs/Logo.png'



const LoginScreen = ({setToken}) => {
    const [Email, setEmail] = useState('');
    const [Password,setPassword] = useState('');
    


    

async function verify(){
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({"emailID":Email,"password":Password});

    var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: raw,
    redirect: 'follow',
    };
    
    return fetch("http://localhost:5000/teacher/login", requestOptions)
        .then(response => response.text())
        // .then(result => setToken(result))
        // .then(data => data.json())
        .catch(error => console.log('error', error));
}

const handleSubmit = async e => {
    e.preventDefault();
    const token = await verify();
    console.log(token);
    setToken(token);
    window.location.reload();
    
  }
  

    return(<div>
        <div className='logoParent'>
        <img src={logo} className="logoChild"/>
        <h1 className="heading" >Softvengers</h1>
        </div>
        <div className='container'>
        <h1>Please Log In</h1>
        <form onSubmit={handleSubmit}>
            
            <div className = 'form-control'>
             <label>Email:</label>
             <input type='text' placeholder = 'Enter Teacher Email' required onChange={(e)=> setEmail(e.target.value)}/>
          </div>
            
            <div className = 'form-control'>
               <label>Password:</label>
               <input type='password' placeholder = 'Enter Password' required onChange={(e)=> setPassword(e.target.value)}/>
           </div>
           <input type = 'submit' value = 'Login'className ='btn2 btn2-block'/> 
        </form>
        </div>
        </div>
        
    )


  }

  export default LoginScreen;

  LoginScreen.propTypes ={
      setToken: PropTypes.func.isRequired
  }