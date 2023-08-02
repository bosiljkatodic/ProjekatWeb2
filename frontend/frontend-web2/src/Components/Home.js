import React from "react";
import Typography from '@mui/material/Typography';
import background from "../Images/home.jpg";

const divStyle = {
    backgroundSize: 'cover',
    width: '100%',
    height: '700px',
    backgroundImage: `url(${background})`,
    backgroundPosition: 'center'
  };
  
const Home = () => {
    return (
       <div style={divStyle}>
        
        <Typography component="h1" variant="h3">
           Dobro došli!
        </Typography>
        <p></p>
        <Typography component="h1" variant="h5">
            Ukoliko ste registrovani, izaberite opciju Log In. 
            <p></p>
            Ukoliko niste registrovani, izaberite opciju Registration.
        </Typography>
       </div>
    );
}

export default Home;