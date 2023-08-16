import '../App.css';
import { NavLink, useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';


const Header = ({isAuth, tipKorisnika, statusVerifikacije, handleLogout}) => {

    const active = (isActive) =>{
        if(isActive)
            return "item active"
        else
            return "item"
    }

    const nav=useNavigate();

    const goToRegistration=()=>{
        nav('registration');
    }

    return (
        <div style={{height:'55px',width:'100%', backgroundColor: '#ededed', borderBottom:'3px solid #b0b0b0'}}>
            {isAuth ? null :
                <Button
                    //className={({isActive}) => active(isActive)}
                    className='headerButton'
                    sx={{m: 1}}
                    variant='contained'               
                    onClick={()=>nav('login')}
                >
                    Log in
                </Button>  
            }     
            {isAuth ? null :      
                <Button
                    //className={({isActive}) => active(isActive)}
                    variant='contained'
                    className='headerButton'
                    onClick={goToRegistration}
                >
                    Registration
                </Button>
            }
            
            {isAuth && tipKorisnika === "Kupac" ? 
                <Button
                    //className={({isActive}) => active(isActive)}
                    variant='contained'
                    className='headerButton'
                    onClick={()=>nav('kupacDashboard')}
                >
                    Kupac Dashboard
                </Button>
                : null
            }

            {isAuth && statusVerifikacije === 'Prihvacen'?
                <Button
                    variant='contained'
                    className='headerButton'
                    onClick={()=>nav('profil')}
                >
                    Profil
                </Button> 
                : null
            }

            {isAuth ? 
            <Button
                variant='contained'
                className='headerButton'
                onClick={handleLogout} 
                href="/"
                >
                    Logout
             </Button> 
             : null}
        </div>
    )
}
export default Header;