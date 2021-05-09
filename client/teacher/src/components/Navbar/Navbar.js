import {useState} from 'react'
import { MenuItems } from "./MenuItems"
import './Navbar.css'
import logo from '../assets/imgs/Logo.png'
import {Button} from '../CustomButtons/Button'



const Navbar = () => {
    const [clicked, setclicked] = useState(false)
    const handleClick = () => setclicked(!clicked)
    
    return (
        <nav className="NavbarItems">
                <img src={logo} className="logo-size"/>
                <h1 className="navbar-logo" >Softvengers</h1>
                <div className="menu-icon" onClick={handleClick}>
                    <i className={clicked ? 'fas fa-times' : 'fas fa-bars'}></i>
                </div>
                <ul className={clicked ? 'nav-menu active' : 'nav-menu'}>
                    {MenuItems.map((item, index) => {
                        return (
                            <li key={index}>
                                <a className={item.cName} href={item.url}>
                                {item.title}
                                </a>
                            </li>
                        )
                    })}
                </ul>
                
            </nav>
    )
}

export default Navbar

