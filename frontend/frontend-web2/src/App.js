import {BrowserRouter,Routes, Route} from 'react-router-dom'
import Login from './Components/AuthComponent/Login';
import Header from './Components/Header';
import Home from './Components/Home';
import Registration from './Components/AuthComponent/Registration'

function App() {
  return (
    <div className="App">
    <BrowserRouter>
    <Header/>
    <Routes>
      <Route path='/' element={<Home/>}/>
      <Route path='/login' element={<Login/>}/>
      <Route path='/registration' element={<Registration/>}/>
    </Routes>
    </BrowserRouter>
    </div>
  );
}

export default App;