import React from 'react';
import ReactDOM from 'react-dom';
import {  Route,  BrowserRouter as Router, Redirect, Switch} from 'react-router-dom';

import './index.css';

import Home from './pages/Home/App';
import NotFound from './pages/notFound/NotFound.js';
import adm from './pages/consultasAdm/ConsultasAdm';
import Login from './pages/Home/App';
import Pacientes from './pages/pacientes/pacientes';
import Medicos from './pages/medicos/medicos';
import mapa from './pages/mapa/map';
import cadastrarMapa from './pages/mapa/cadastroMapa';
import Header from './pages/Header/header';
import Perfil from './pages/perfil/perfil';
import Cadastro from './pages/cadastro/cadastro';


import reportWebVitals from './reportWebVitals';

const routing=(
  <Router>
    <div>
      <Switch>
      <Route exact path="/" component={ Home } />
      <Route  path="/consultasAdm" component={ adm } />
      <Route  path="/login" component={ Login } />
      <Route  path="/paciente" component={ Pacientes } />
      <Route  path="/medicos" component={ Medicos } />
      <Route  path="/cadastrarMapa" component={ cadastrarMapa } />
      <Route  path="/notfound" component={ NotFound } />      
      <Route  path="/perfil" component={ Perfil } />      
      <Route  path="/mapa" component={ mapa } />      
      <Route  path="/header" component={ Header } />      
      <Route  path="/cadastrar" component={ Cadastro } />      
      <Redirect to="/notfound" />
      </Switch>
     
    </div>
  </Router>
)

ReactDOM.render(  routing,  document.getElementById('root'));

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
