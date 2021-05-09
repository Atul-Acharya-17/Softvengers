
import Routes from '../../Routes'
import Particles from 'react-particles-js';
import { Component } from 'react';
import LoginScreen from './LoginScreen';
import useToken from '../../variables/useToken'
import {BrowserRouter, Redirect, Route,Router} from 'react-router-dom'
import App from '../../App'

function Logout (props){
    sessionStorage.clear();
    props.setShowNavbar(false);
  return(
      <BrowserRouter>
        <Route exact path ="" component = {App}/>
        <Redirect exact path to="" />
      </BrowserRouter>
    // <div className="wrapper">
    //   <Routes/>
    // </div>
  );
    
}

export default Logout; 