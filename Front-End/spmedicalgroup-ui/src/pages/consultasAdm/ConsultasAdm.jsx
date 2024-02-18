import React from 'react';
import { Link } from "react-router-dom";

import '../../Assets/CSS/header.css'
import api from '../../services/api';
import '../../Assets/CSS/consultasAdm.css';
import logo from '../../Assets/img/Sp Medical Grouplogo.svg';
import HeaderAdm from '../../components/header/headerAdm';


class consultasAdm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            listaConsultas: [],
            listaPacientes: [],
            listaMedicos: [],
            listaSituacao: [1, 2, 3],
            idSituacao: 0,
            idMedico: 0,
            idPaciente: 0,
            descricao: '',
            dataConsulta: new Date(),
            active: false,
            isLoading: false
        };
    };

    toggleMode = () => {
        this.setState({ active: !this.state.active })
    }

    buscarMedicos = async () => {
        try {

            const resposta = await api.get('/Medicos', {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
                },
            })
            if (resposta.status === 200) {
                this.setState({ listaMedicos: resposta.data });
            }
        }

        catch (error) {
            console.warn(error)
        }
    };

    buscarPacientes = async () => {
        try {

            const resposta = await api.get('/Pacientes', {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
                }
            })
            if (resposta.status === 200) {
                this.setState({ listaPacientes: resposta.data.lista });
            }
        }

        catch (error) {
            console.warn(error)
        }
    };

    buscarConsulta = async () => {
        try {


            const resposta = await api.get('/Consultas', {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
                }
            })
            if (resposta.status === 200) {
                this.setState({ listaConsultas: resposta.data });
            }
        }

        catch (error) {
            console.warn(error)
        }


    };


    componentDidMount() {
        this.buscarConsulta();
        this.buscarMedicos();
        this.buscarPacientes();
    };

    logout = async () => {
        localStorage.removeItem('usuario-login');
        this.props.history.push('/');
    };



    atualizaStateCampo = (campo) => {
        this.setState({ [campo.target.name]: campo.target.value });
    };

    deletarConsulta = (consulta) => {
        api.delete('/Consultas/Remover/' + consulta.idConsulta, {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
            }
        })
            .then((resposta) => {
                if (resposta.status === 204) {
                    console.log(
                        'Consulta ' + consulta.idConsulta + ' foi excluída!',
                    );
                }
            })
            .catch((erro) => console.log(erro))

            .then(this.buscarConsulta);

    };
    cancelarConsulta = (consulta) => {
        console.log(consulta.idConsulta)
        api.patch('/Consultas/Cancelar/' + consulta.idConsulta, {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
            }
        })
            .then((resposta) => {
                if (resposta.status === 200) {
                    console.log(
                        'Essa Consulta ' + consulta.idConsulta + ' foi cancelada!'
                    );
                }
            })
            .catch((erro) => console.log(erro))
            .then(this.buscarConsulta);

    };


    cadastrarConsulta = item => {
        try {
            this.setState({ isLoading: true });



            const resposta = api.post('/Consultas', {
                idPaciente: this.state.idPaciente,
                idMedico: this.state.idMedico,
                idSituacao: this.state.idSituacao,
                descricao: this.state.descricao,
                dataConsulta: new Date(this.state.dataConsulta)
            }, {
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
                },
            }

            );
            if (resposta.status === 201) {
                console.warn('Consulta realizada com sucesso.');
            } else {
                console.warn('Falha ao Cadastrar Consulta.');
            }
        } catch (error) {
            console.warn(error);
        }


    };


    render() {
        return (
            //JSX
            <div>

                <header>
                    <HeaderAdm />
                </header>

                <main className="afastar_list_consulta ">
                    {/* Lista de tipos de consulta */}
                    <section className="lista_consulta grid">
                        <h2 id="lista">Lista de Consultas</h2>
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
                                    this.state.listaConsultas.map((consulta) => {
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
                                                <td>
                                                    <button className='acoes_btn btn' onClick={() => this.cancelarConsulta(consulta)}>Cancelar</button>
                                                    <button className='acoes_btn btn' onClick={() => this.deletarConsulta(consulta)}>Excluir</button>
                                                </td>

                                            </tr>


                                        )
                                    })
                                }
                            </tbody>
                        </table>
                    </section>


                    {/* Cadastro de tipos de consulta */}
                    <div className="afastar_list_consulta" id="cadastro">
                        <section className="cadastro_consulta grid ">
                            <h2 className="letra_tam" >Cadastro de Consultas</h2>
                            <div className="cadastro_ajuste_consulta">
                                <form onSubmit={this.cadastrarConsulta} >
                                    <div>

                                        <select
                                            className="input_consulta"
                                            name="idPaciente"
                                            value={this.state.idPaciente}
                                            onChange={this.atualizaStateCampo}>
                                            <option value="0">Selecione o paciente</option>

                                            {
                                                this.state.listaPacientes.map((paciente) => {
                                                    return (
                                                        <option key={paciente.idPaciente} value={paciente.idPaciente}>
                                                            {paciente.idUsuarioNavigation.nome}
                                                        </option>
                                                    )
                                                })
                                            }
                                        </select>

                                        <select
                                            className="input_consulta"
                                            name="idMedico"
                                            value={this.state.idMedico}
                                            onChange={this.atualizaStateCampo}>
                                            <option value="0">Selecione o Médico</option>

                                            {
                                                this.state.listaMedicos.map((medico) => {
                                                    return (
                                                        <option key={medico.idMedico} value={medico.idMedico}>
                                                            {medico.idUsuarioNavigation.nome}
                                                        </option>
                                                    )
                                                })
                                            }
                                        </select>
                                        {/* <select
                                            className="input_consulta"
                                            name="idSituacao"
                                            value={this.state.idSituacao}
                                            onChange={this.atualizaStateCampo}>
                                            <option value="0">Selecione a Situação</option>

                                            <option value={this.state.listaSituacao[0]}>Agendada</option>
                                            <option value={this.state.listaSituacao[1]}>Cancelada</option>
                                            <option value={this.state.listaSituacao[2]}>Realizada</option>


                                        </select> */}



                                        <input
                                            className="input_consulta"
                                            type="datetime-local"
                                            name="dataConsulta"
                                            value={this.state.dataConsulta}
                                            onChange={this.atualizaStateCampo}
                                        />

                                        <div className="btn_cadastrar_consulta">

                                            {this.state.isLoading && (
                                                <button className="btn_consultaAdm " disabled>
                                                    Loading...{' '}
                                                </button>
                                            )}

                                            {this.state.isLoading === false && (
                                                <button className="btn_consultaAdm btn" >Cadastrar</button>
                                            )}
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </section>
                    </div>


                </main>

                <footer>
                    <div className="container_footer afastar_list_consulta">
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
            </div >
        )
    }
};
export default consultasAdm;