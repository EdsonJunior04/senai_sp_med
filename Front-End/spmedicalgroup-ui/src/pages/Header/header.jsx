import '../../Assets/CSS/header.css'
import { useState } from 'react'
import {  useHistory } from "react-router-dom";


export default function Header() {

    const [active, setMode] = useState(false);
    const ToggleMode = () => {
        setMode(!active)
    }

    const navigation = useHistory();

    const logout = () => {
        localStorage.removeItem('usuario-login');
        navigation.push('/')
    }

    return (
        <div className='App'>
            <div className={active ? "icon iconActive" : "icon"} onClick={ToggleMode}>
                <div className="hamburguer hamburguerIcon"></div>
            </div>
            <div className={active ? 'menu menuOpen ' : 'menu menuClose'}>
                <div className='list '>
                    <ul className='listItems'>
                        <li>PERFIL</li>
                        <li>ALTERAR DESCRIÇÃO</li>
                        <li>LISTAR CONSULTAS</li>
                        <li><button className='btn_sair btn' onClick={logout} >Sair</button></li>
                    </ul>
                </div>
            </div>
        </div>
    )
}