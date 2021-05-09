import './App.css'
import Routes from './Routes'
import LoginScreen from './components/Views/LoginScreen';
import useToken from './variables/useToken'




const App = () => {
  
  const { token, setToken } = useToken();
  
  if (!token || token === 'undefined' ||token ===''){
    return <LoginScreen setToken={setToken} />
  }
  else if (token === 'Username does not exist' || token === 'Password is incorrect for the user')
    {
      return(
       
        <div>
          {(window.confirm(token))?
             <LoginScreen setToken={setToken} />:(window.confirm(token))}
        
        </div>
         
      )
    }
  console.log(token)
  return(
    <div>
      <Routes/>
    </div>
  );


  
}



export default App;