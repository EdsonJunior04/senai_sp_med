import { useState } from 'react';
import { useHistory } from "react-router-dom";
import '../../Assets/CSS/header.css'
import '../../Assets/CSS/paciente.css';

import PerfilFoto from '../../components/perfilfoto/perfilfoto';
import logo from '../../Assets/img/Sp Medical Grouplogo.svg';

export default function HeaderAdm() {
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
                                    <a className='Link' href="/perfil"><li>Perfil</li></a>
                                    <a className='Link' href="/consultas#lista"><li>Consultas</li></a>
                                    <a className='Link' href="/consultasAdm#cadastro"><li>Cadastrar Consultas</li></a>
                                    <a className='Link' href="/mapa"><li>Mapas</li></a>
                                    <a className='Link' href="/cadastrarMapa"><li>Cadastrar Localização</li></a>
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
                <p>Administrador</p>
                <PerfilFoto />
            </div>
        </div>

    )
}