import { useState } from 'react';
import { Link, useHistory } from "react-router-dom";

import '../../Assets/CSS/paciente.css';

import PerfilFoto from '../../components/perfilfoto/perfilfoto';
import logo from '../../Assets/img/Sp Medical Grouplogo.svg';

export default function HeaderPaciente() {
    const [active, setMode] = useState(false);
    const navigation = useHistory();
    const ToggleMode = () => {
        setMode(!active)
    }

    const logout = () => {
        localStorage.removeItem('usuario-login');
        navigation.push('/')
    }

    return (
        <div>

            
                <div className='end'>
                    <div className="container_header_paciente">
                        <div>
                            <div className={active ? "icon iconActive" : "icon"} onClick={ToggleMode}>
                                <div className="hamburguer hamburguerIcon"></div>
                            </div>
                            <div className={active ? 'menu menuOpen ' : 'menu menuClose'}>
                                <div className='list '>
                                    <ul className='listItems'>
                                        <Link className='Link' to="/perfil"><li>Perfil</li></Link>
                                        <a className='Link' href="#cadastro"><li>CADASTRAR CONSULTA</li></a>
                                        <a className='Link' href="#lista"><li>LISTAR CONSULTAS</li></a>
                                        <Link className='Link' to="/mapa"><li>MAPAS</li></Link>
                                        <Link className='Link' to="/cadastrarMapa"><li>CADASTRAR LOCALIZAÇÃO</li></Link>
                                        <li><button className='btn_sair btn' onClick={logout} >Sair</button></li>
                                    </ul>
                                </div>
                            </div>
                        </div>


                        <img
                            src={logo}
                            className="icone_paciente"
                            alt="logo da Sp Medical Group"
                        />{' '}
                    </div>
                    <p>Paciente</p>
                    <PerfilFoto />
                </div>
        </div>

    )
}