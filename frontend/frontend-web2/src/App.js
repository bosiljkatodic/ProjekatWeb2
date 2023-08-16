import {BrowserRouter,Routes, Route} from 'react-router-dom'
import Login from './Components/AuthComponent/Login';
import Header from './Components/Header';
import Home from './Components/Home';
import Registration from './Components/AuthComponent/Registration'
import React, { useState, useEffect } from 'react'; 
import KupacDashboard from './Components/KupacDashboard';
import Profil from './Components/Profil';

function App() {

//da li je korisnik autentifikovan, on je atuentifikovan i posle registracije i posle logovanje
const [isAuth, setIsAuth] = useState(false);
const [tipKorisnika, setTipKorisnika] = useState('');
const [statusVerifikacije, setStatusVerifikacije] = useState('');
const [isKorisnikInfoGot, setIsKorisnikInfoGot] = useState(false);  //ovo govori da li smo dobili podatke o korisniku

useEffect(() => {
  const getAuth = () => {
      if(sessionStorage.getItem('korisnik') !== null && sessionStorage.getItem('isAuth') !== null){
          setIsAuth(JSON.parse(sessionStorage.getItem('isAuth')))
          const korisnik = JSON.parse(sessionStorage.getItem('korisnik'))
          setTipKorisnika(korisnik.tipKorisnika);
          setStatusVerifikacije(korisnik.verifikacijaProdavca);
      }
  }
  getAuth();
}, [isKorisnikInfoGot]); //kada dobijemo ove podatke, ova funkcija ce se rerenderovati i onda ce se azurirati stanja
                          //na taj nacin izqazvacemo ponovno azuriranje stranice i onda navbara, nadam se da je tako

const handleKorisnikInfo = (gotKorisnikInfo) => {
  setIsKorisnikInfoGot(gotKorisnikInfo);
}

const handleLogout = () => {
  sessionStorage.removeItem('korisnik');
  sessionStorage.removeItem('isAuth');
  sessionStorage.removeItem('token');
  setIsAuth(false);
  setStatusVerifikacije('');
  setTipKorisnika('');
  setIsKorisnikInfoGot(false);  
}

  return (
    <div className="App">
    <BrowserRouter>
    <Header isAuth={isAuth} tipKorisnika = {tipKorisnika} statusVerifikacije={statusVerifikacije} handleLogout={handleLogout}/>
    <Routes>
      <Route path='/' element={<Home></Home>}/>
      <Route path='/login' element={<Login handleKorisnikInfo={handleKorisnikInfo}></Login>}/>
      <Route path='/registration' element={<Registration handleKorisnikInfo={handleKorisnikInfo}></Registration>}/>
      <Route path='/kupacDashboard' element={<KupacDashboard></KupacDashboard>}/>
      <Route path='/profil' element={<Profil></Profil>} />
    </Routes>
    </BrowserRouter>
    </div>
  );
}

export default App;