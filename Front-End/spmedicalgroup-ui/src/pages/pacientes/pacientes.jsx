import { useState, useEffect } from 'react';
import { Link } from "react-router-dom";

import '../../Assets/CSS/paciente.css';
import HeaderPaciente from '../../components/header/headerPaciente';
import logo from '../../Assets/img/Sp Medical Grouplogo.svg';
import api from "../../services/api";

export default function Pacientes() {
    const [listaMinhasConsultas, setMinhasConsultas] = useState([]);
    


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

    

    return (
        <div>

            <header>
                <HeaderPaciente />

            </header>

            <main className="afastar_list_paciente ">
                {/* Lista de tipos de consulta */}
                <section className="lista_paciente grid">
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
                                        </tr>
                                    )
                                })
                            }
                        </tbody>
                    </table>
                </section>
            </main>
            <footer className="espaco_paciente">
                <div className="container_footer">
                    <div className="center_footer">
                        <Link to="/">
                            <img
                                src={logo}
                                className="icone_consulta_footer"
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