import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import api from "../../services/api";
import '../../Assets/CSS/header.css'
import HeaderMedico from "../../components/header/headerMedico";



import '../../Assets/CSS/medicos.css';

import logo from '../../Assets/img/Sp Medical Grouplogo.svg';
export default function Medicos() {
    const [listaMinhasConsultas, setMinhasConsultas] = useState([])
    const [idConsulta, setIdConsultas] = useState(0)
    const [idMedico] = useState(0)
    const [descricao, setDescricao] = useState('')
    const [isLoading, setisLoading] = useState(false)  
    

   

    function realizarConsulta(id) {
        console.log(id)
        api.put('/Consultas/Realizar/' + id, {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
            }
        })
            .then((resposta) => {
                if (resposta.status === 200) {
                    console.log(
                        'Essa Consulta ' + id + ' foi realizada!',
                    );
                    buscarMinhasConsultas()
                }
            })
            .catch((erro) => console.log(erro))
    };

    


    function buscarMinhasConsultas() {
        api('/Consultas/Lista/Minhas', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        })

            .then(resposta => {
                if (resposta.status === 200) {
                    setMinhasConsultas(resposta.data.listaConsulta)
                }
            })
            .catch(erro => console.log(erro))
    }
    useEffect(buscarMinhasConsultas, [])

    function alterarDescricao(event) {
        setisLoading(true)

        let novadescricao = {
            idMedico: idMedico,
            descricao: descricao
        }

        api.patch('/Consultas/AlterarDescricao/' + idConsulta, novadescricao, {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
            }
        })
            .then((resposta) => {
                if (resposta.status === 200) {
                    console.log('Descrição Alterada')

                    buscarMinhasConsultas();
                    setisLoading(false);
                }
            })
            .catch(erro => console.log(erro), setInterval(() => {
                setisLoading(false);
            }, 5000));
    }

    return (
        <div>

            <header>
                <HeaderMedico />

            </header>

            <main className="afastar_list_medico ">
                {/* Lista de tipos de consulta */}
                <section className="lista_medico grid_medico afastar">
                    <h2>Minhas Consultas</h2>
                    <table>
                        <thead >
                            <tr>
                                <th>#</th>
                                <th>Situação</th>
                                <th>Paciente</th>
                                <th>Médico</th>
                                <th>Descrição</th>
                                <th>Data</th>
                            </tr>
                        </thead>
                        <tbody >
                            {
                                listaMinhasConsultas.map((consulta) => {
                                    return (
                                        <tr key={consulta.idConsulta} >
                                            <td>{consulta.idConsulta}</td>
                                            <td>{consulta.idSituacaoNavigation.descricao}</td>
                                            <td>{consulta.idPacienteNavigation.idUsuarioNavigation.nome}</td>
                                            <td>{consulta.idMedicoNavigation.idUsuarioNavigation.nome}</td>
                                            <td>{consulta.descricao}</td>
                                            <td>{Intl.DateTimeFormat("pt-BR", {
                                                year: 'numeric', month: 'short', day: 'numeric',
                                                hour: 'numeric', minute: 'numeric', hour12: false
                                            }).format(new Date(consulta.dataConsulta))}</td>
                                            <td><button className='realizar_btn btn' onClick={() => realizarConsulta(consulta.idConsulta)}>Realizada</button></td>
                                        </tr>
                                    )
                                })
                            }
                        </tbody>
                    </table>
                </section>

                <div className="lista_medico grid_medico espaco">
                    <h2 className="letra_tama">Alterar Descrição</h2>
                    <form onSubmit={alterarDescricao}>
                        <select
                            className="input_alterar"
                            name="consulta"
                            id="consulta"
                            value={idConsulta}
                            onChange={(campo) => setIdConsultas(campo.target.value)}
                        >
                            <option value="0">Selecione a Consulta</option>

                            {
                                listaMinhasConsultas.map((consulta) => {
                                    return (
                                        <option key={consulta.idConsulta} value={consulta.idConsulta}>
                                            {consulta.idPacienteNavigation.idUsuarioNavigation.nome} / Cpf:{consulta.idPacienteNavigation.cpf} / Data:{Intl.DateTimeFormat("pt-BR", {
                                                year: 'numeric', month: 'numeric', day: 'numeric',
                                                hour: 'numeric', minute: 'numeric', hour12: false
                                            }).format(new Date(consulta.dataConsulta))}
                                        </option>
                                    )
                                })}
                        </select>
                        <input className="input_alterar arru" type="text" value={descricao} onChange={(campo) => setDescricao(campo.target.value)} placeholder="Nova Descrição" />

                        <div className="btn_cadastrar_medico">
                            {isLoading && (
                                <button disabled className='btn_medico' type='submit'>
                                    Carregando...
                                </button>
                            )}
                            {!isLoading && (
                                <button className='btn_medico btn' type='submit'>
                                    Alterar
                                </button>
                            )}
                        </div>
                    </form>
                </div>
            </main>
            <footer className="espaco">
                <div className="container_footer">
                    <div className="center_footer">
                        <Link to="/">
                            <img
                                src={logo}
                                className="icone_medico_footer"
                                alt="logo da Sp Medical Group"
                            />{' '}
                        </Link>
                        <span className="span_footer">Feito por Senai de Informática</span>
                    </div>
                </div>
            </footer>
        </div>
    )
}
