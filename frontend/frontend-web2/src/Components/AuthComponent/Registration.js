import * as React from 'react';
import { useState, useEffect} from "react";
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import {Link} from "react-router-dom";
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { useNavigate } from "react-router-dom";
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import UserImage from '../UserImage';
import image from "../../Images/camera.png";

// TODO remove, this demo shouldn't need to reset the theme.

const defaultTheme = createTheme();

export default function Registration() {
  const handleSubmit = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log({
      email: data.get('email'),
      password: data.get('password'),
    });
  };

  const [tipKorisnika, setTipKorisnika] = useState('Kupac');
  const [cijenaDostave, setCijenaDostave] = useState(0);
  const[error, setError] = useState(false);
  const [slika, setSlika] = useState(image);

  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 0,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
           Registracija
          </Typography>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12} sm={6}>
                <TextField
                  autoComplete="given-name"
                  name="ime"
                  required
                  fullWidth
                  id="ime"
                  label="Ime"
                  autoFocus
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  required
                  fullWidth
                  id="lastName"
                  label="Prezime"
                  name="lastName"
                  autoComplete="family-name"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="username"
                  label="Korisiničko ime"
                  name="username"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="Email "
                  name="email"
                  autoComplete="email"
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DatePicker />
                </LocalizationProvider>
              </Grid>
              <Grid item xs={12} sm={6}>
                <FormControl fullWidth>
                    <InputLabel id="demo-simple-select-label">Tip korisnika</InputLabel>
                    <Select
                        required
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={tipKorisnika}
                        label="Tip korisnika"
                        onChange={(e) => setTipKorisnika(e.target.value)}
                    >
                    <MenuItem value="Prodavac">Prodavac</MenuItem>
                    <MenuItem value="Kupac">Kupac</MenuItem>
                    </Select>
                </FormControl>
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="adresa"
                  label="Adresa"
                  name="adresa"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Lozinka"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password2"
                  label="Ponovi lozinku"
                  type="password"
                  id="password2"
                  autoComplete="new-password"
                />
              </Grid>
              <Grid item xs={12}>
                <UserImage slika={slika} setSlika={setSlika}></UserImage>   
                {error && slika.length === 0 ? <div className="ui pointing red basic label">Morate odabrati sliku</div> : null} 
              </Grid> 
              {tipKorisnika === "Prodavac" ? 
                        <Grid item xs={12}>
                            <label>Potrebno je unijeti cijenu dostave</label>
                            <TextField
                                    required
                                    fullWidth
                                    name="cijenaDostave" 
                                    placeholder="Cijena dostave"
                                    value={cijenaDostave}
                                    type="number" 
                                    onChange={(e) => setCijenaDostave(e.target.value)}
                            />
                            {error && cijenaDostave === "" ? <div className="ui pointing red basic label">Unesite cijenu dostave</div> : null}
                        </Grid>
                            : null}
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Registruj me
            </Button>
            <Link to="/login"> Već imate nalog? Prijavi se </Link>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}