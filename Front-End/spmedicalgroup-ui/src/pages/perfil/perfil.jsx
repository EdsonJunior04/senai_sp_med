import { React, Component } from 'react';
import { Link } from 'react-router-dom';
import logo from '../../Assets/img/Sp Medical Grouplogo.svg';
import api from '../../services/api';
import Titulo from '../../components/titulo/titulo';
import PerfilFoto from '../../components/perfilfoto/perfilfoto';
import '../../Assets/CSS/perfil.css'
export default class Perfil extends Component {
    constructor(props) {
        super(props);
        this.state = {
            imagem64: '',
            active: false,
            arquivo: null,
        };
    }
    upload = () => {
        const formData = new FormData();
        formData.append(
            'arquivo',
            this.state.arquivo,
        );

        api.post('/Perfils/imagem/bd', formData, {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
            },
        })
            .catch((erro) => console.log(erro))
            .then(this.buscarImagem);
    };

    logout = async () => {
        localStorage.removeItem('usuario-login');
        this.props.history.push('/');
    };

    atualizaState = (event) => {
        console.log('foto');
        this.setState({ arquivo: event.target.files[0] }, () => {
            console.log(this.state.arquivo)
        });
    };

    toggleMode = () => {
        this.setState({ active: !this.state.active })
    }

    buscarImagem = () => {
        api('/Perfils/imagem/bd',
         {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
            },
        }
        )
            .catch((erro) => console.log(erro))
            .then((resposta) => {
                if (resposta.status === 200) {
                    console.log(resposta);
                    this.setState({ imagem64: resposta.data });
                }
            });
    };

    componentDidMount() {
        this.buscarImagem();
    }

    render() {
        return (
            <div>

                <header>
                    <div className='end'>
                        <div className="container_header_paciente">
                            <div>
                                <div className={this.state.active ? "icon iconActive" : "icon"} onClick={this.toggleMode}>
                                    <div className="hamburguer hamburguerIcon"></div>
                                </div>
                                <div className={this.state.active ? 'menu menuOpen ' : 'menu menuClose'}>
                                    <div className='list '>
                                        <ul className='listItems'>
                                            <Link className='Link' to="/perfil"><li>PERFIL</li></Link>
                                            <a className='Link' href="/consultasAdm#cadastro"><li>CADASTRAR CONSULTA</li></a>
                                            <a className='Link' href="/consultasAdm#lista"><li>LISTAR CONSULTAS</li></a>
                                            <Link className='Link' to="/cadastrarMapa"><li>CADASTRAR LOCALIZAÇÃO</li></Link>
                                            <li><button className='btn_sair btn' onClick={this.logout} >Sair</button></li>
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
                        <p>ADIMINISTRADOR</p>
                        <PerfilFoto />
                    </div>

                </header>

                <main className="afastar_list_paciente">
                    <section className="lista_paciente grid">
                        <Titulo titulosecao="Imagem do Perfil" />
                        <div className="container_perfil" id="conteudoPrincipal-lista">
                            <h2>Upload de Imagem</h2>
                            <input  type="file" onChange={this.atualizaState} />

                            {
                                this.state.arquivo === null ?
                                    <button disabled className="acoes_btn btn" onClick={this.upload} >Enviar! </button>
                                    :
                                    <button className="acoes_btn btn" onClick={this.upload} >Enviar! </button>
                            }


                        </div>
                        <div className="container_perfil" id="conteudoPrincipal-lista">
                            <img className="imagem_header"
                                src={`data:image;base64,${this.state.imagem64}`}
                                alt="Imagem de Perfil"
                            />
                        </div>
                    </section>
                </main>
                <footer className="espaco_paciente">
                <div className="container_footer">
                    <div className="center_footer">
                        
                            <img
                                src={logo}
                                className="icone_consulta_footer"
                                alt="logo da Sp Medical Group"
                            />{' '}
                        
                        <span className="span_footer">Feito por Senai de Informática</span>
                    </div>
                </div>
            </footer>
            </div>
        )
    }

}