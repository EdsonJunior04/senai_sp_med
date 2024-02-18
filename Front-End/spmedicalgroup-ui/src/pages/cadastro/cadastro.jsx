import { useState } from "react";
import { Link } from "react-router-dom";
import logo from '../../Assets/img/Sp Medical Grouplogo.svg';
import '../../Assets/CSS/cadastro.css'
import api from "../../services/api";
export default function Cadastro() {
    const [idTIpoUsuario, setIdTipoU] = useState(0)
    const [isLoading, setisLoading] = useState(false)
    const [nome, setNome] = useState('')
    const [email, setEmail] = useState('')
    const [senha, setSenha] = useState('')
    const [, setErrorMsg] = useState('')

    const listaTipoU = [1, 2, 3]

    function efetuarCadastro(event) {
        event.preventDefault();
        api.post('/Usuarios', {
            IdTIpoUsuario: idTIpoUsuario,
            Nome: nome,
            Email: email,
            senha: senha
        })
            .then(resposta => {
                if (resposta.status === 201) {
                    console.log('Cadastro Realizado')
                }
            })
            .catch(() => {
                setErrorMsg('Cadastro não realizado!')

                setisLoading(false)

            });


    }

    return (
        <div className="fundo_cadastro">
            <main>
                <div className="center-login">


                    <img
                        src={logo}
                        className="icone__login"
                        alt="logo da Sp Medical Group"
                    />{' '}

                    <form
                        onSubmit={efetuarCadastro}
                    >
                        <div className="center-login">


                            <select
                                className="select_cadastro"
                                name="idTipoU"
                                value={idTIpoUsuario}
                                onChange={(campo) => setIdTipoU(campo.target.value)}
                            >
                                <option value="0">Selecione o Tipo de Usuário</option>

                                <option value={listaTipoU[0]}>Médico</option>
                                <option value={listaTipoU[1]}>Paciente</option>
                                <option value={listaTipoU[2]}>Administrador</option>


                            </select>
                            <input
                                className="input_login"
                                type="text"
                                name="Nome"
                                value={nome}
                                placeholder=" Nome"
                                onChange={(campo) => setNome(campo.target.value)}
                            />
                            <input
                                className="input_login"
                                type="email"
                                name="email"
                                value={email}
                                placeholder=" E-mail"
                                onChange={(campo) => setEmail(campo.target.value)}
                            />

                            <input
                                className="input_login"
                                type="password"
                                name="senha"
                                value={senha}
                                placeholder=" senha"
                                onChange={(campo) => setSenha(campo.target.value)}
                            />

                            <div className="erroMensagem">

                                <text style={{ color: 'red' }} >{setErrorMsg}</text>
                            </div>

                            {isLoading && (
                                <button disabled className='btn_medico' type='submit'>
                                    Carregando...
                                </button>
                            )}
                            {!isLoading && (
                                <button className='btn_medico btn' type='submit'>
                                    Cadastrar-se
                                </button>
                            )}

                            <div className="cadastre">Já tem conta?    <Link to="/" className="cadastrar">Faça o login</Link></div>


                        </div>
                    </form>
                </div>
            </main >
        </div >
    )
}