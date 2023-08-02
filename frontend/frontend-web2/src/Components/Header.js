import '../App.css';
import { useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';


function Header(){
    const nav=useNavigate();
    const goToRegistration=()=>{
        nav('registration');
    }
    return (
        <div style={{height:'55px',width:'100%', backgroundColor: '#ededed', borderBottom:'3px solid #b0b0b0'}}>
        
            <Button
                sx={{m: 1}}
                variant='contained'
                className='headerButton'
                onClick={()=>nav('login')}
            >
                Log in
            </Button>  
                     
            <Button
                variant='contained'
                className='headerButton'
                onClick={goToRegistration}
            >
                Registration
            </Button>
           
        </div>
    )
}
export default Header;