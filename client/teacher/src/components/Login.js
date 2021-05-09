import react, { useState , Component} from 'react';
import {axios} from 'axios';
import MuiThemeProvider, {UploadScreen}from 'material-ui/styles/MuiThemeProvider';
import AppBar from 'material-ui/AppBar';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';
import Navbar from './Navbar/Navbar';
// import PropTypes from 'prop-types';

// async function loginUser(credentials) {
//   return fetch("localhost:5000/teacher/login", {
//     method: 'POST',
//     headers: {
//       'Content-Type': 'application/json'
//     },
//     body: JSON.stringify(credentials)
//   })
//     .then(data => data.json())
//  }

// export default function Login({ setToken }) {
//   const [username, setUserName] = useState();
//   const [password, setPassword] = useState();
//   const handleSubmit = async e => {
//     e.preventDefault();
//     const token = await loginUser({
//       username,
//       password
//     });
//     setToken(token);
//   }
//   return(
//     <div className="login-wrapper">
//       <h1>Please Log In</h1>
//       <form onSubmit={handleSubmit}>
//         <label>
//           <p>Username</p>
//           <input type="text" onChange={e => setUserName(e.target.value)}/>
//         </label>
//         <label>
//           <p>Password</p>
//           <input type="password" onChange={e => setPassword(e.target.value)}/>
//         </label>
//         <div>
//           <button type="submit">Submit</button>
//         </div>
//       </form>
//     </div>
//   )
// }

// Login.propTypes = {
//   setToken: PropTypes.func.isRequired
// }

class Login extends Component {
constructor(props){
  super(props);
  this.state={
  email:'',
  password:''
  }
 }
 handleClick(event){
  var apiBaseUrl = "localhost:5000/teacher/login=";
  var self = this;
  var payload={
  "email":this.state.email,
  "password":this.state.password
  }
  axios.post(apiBaseUrl+'login', payload)
  .then(function (response) {
  console.log(response);
  if(response.data.code == 200){
  console.log("Login successfull");
  var Nav=[];
  Nav.push(<Navbar/>)
  self.props.appContext.setState({loginPage:[],Nav:Navbar})
  }
  else if(response.data.code == 204){
  console.log("Username password do not match");
  alert("username password do not match")
  }
  else{
  console.log("Username does not exists");
  alert("Username does not exist");
  }
  })
  .catch(function (error) {
  console.log(error);
  });
  }
render() {
    return (
      <div>
        <MuiThemeProvider>
          <div>
          <AppBar
             title="Login"
           />
           <TextField
             hintText="Enter your Username"
             floatingLabelText="Username"
             onChange = {(event,newValue) => this.setState({username:newValue})}
             />
           <br/>
             <TextField
               type="password"
               hintText="Enter your Password"
               floatingLabelText="Password"
               onChange = {(event,newValue) => this.setState({password:newValue})}
               />
             <br/>
             <RaisedButton label="Submit" primary={true} style={style} onClick={(event) => this.handleClick(event)}/>
         </div>
         </MuiThemeProvider>
      </div>
    );
  }
}
const style = {
 margin: 15,
};


export default Login;

