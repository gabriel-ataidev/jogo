# 🎮 Dev Rush

**Jogo educativo de linguagens de programação** desenvolvido como projeto final da disciplina XDES17 – Introdução à Codificação de Jogos Digitais (UNIFEI).

![Unity](https://img.shields.io/badge/Unity-6000.0-black?logo=unity)
![C#](https://img.shields.io/badge/C%23-9.0-purple?logo=csharp)

## 📖 Sobre o Jogo

Dev Rush é um **runner 2D** onde o jogador aprende características de cinco linguagens de programação (Python, JavaScript, C#, Go e Swift) de forma interativa e lúdica.

### 🎯 Objetivo
Coletar os ícones da linguagem correta baseado nas dicas contextuais apresentadas, evitando as linguagens erradas.

### 🏆 Progressão de Carreira
- **Fase 1**: Estagiário
- **Fase 2**: Junior
- **Fase 3**: Pleno
- **Fase 4**: Senior
- **Fase 5**: DEUS DOS CÓDIGOS

## 🚀 Como Jogar

### Controles
- **← →** ou **A D**: Movimento horizontal
- **Espaço**: Confirmar/Iniciar
- **S**: Sair do jogo

### Regras
- ✅ Colete **5 ícones corretos** para passar de fase
- ❌ Coletar ícone errado = **Game Over**
- ⏱️ Timer de **60 segundos** por fase

## 💻 Requisitos para Compilação

### Software Necessário
- **Unity 6** (6000.0.x ou superior)

### Passos para Compilar

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/SEU_USUARIO/dev-rush.git
   ```

2. **Abra o projeto no Unity Hub**:
   - Clique em "Add" → "Add project from disk"
   - Selecione a pasta do projeto clonado

3. **Aguarde a importação**:
   - O Unity importará todos os assets automaticamente
   - Isso pode levar alguns minutos na primeira vez

4. **Abra a cena principal**:
   - Navegue até `Assets/Scenes/`
   - Abra a cena `SampleScene` (ou a cena principal do jogo)

5. **Execute o jogo**:
   - Pressione o botão **Play** ▶️ no Unity Editor

### Build para Windows

1. Vá em **File → Build Settings**
2. Selecione **Windows, Mac, Linux** como plataforma
3. Clique em **Build** e escolha a pasta de destino

## 📁 Estrutura do Projeto

```
Assets/
├── Scripts/
│   ├── GameManager.cs          # Estado do jogo
│   ├── PhaseManager.cs         # Gerenciamento de fases
│   ├── PhaseData.cs            # Dados das linguagens
│   ├── PhaseFeedbackManager.cs # Feedback de sucesso/erro
│   ├── NinjaController.cs      # Controle do jogador
│   ├── LanguageSpawner.cs      # Geração de ícones
│   ├── HUDController.cs        # Interface do usuário
│   └── CodeTypeController.cs   # Efeito typewriter
├── Prefabs/                    # Prefabs das linguagens
├── Scenes/                     # Cenas do jogo
├── Audio/                      # Efeitos sonoros
└── Sprites/                    # Imagens e ícones
```

## 🛠️ Tecnologias Utilizadas

- **Engine**: Unity 6 (6000.0.x)
- **Linguagem**: C# 9.0
- **Renderização**: Universal Render Pipeline (URP) 2D
- **UI**: TextMeshPro
- **Input**: Unity Input System

## 👥 Autores

- **Gabriel S. Ataide** - [d2022004770@unifei.edu.br](mailto:d2022004770@unifei.edu.br)
- **Isaac P. Almeida** - [isaacpalmeida@unifei.edu.br](mailto:isaacpalmeida@unifei.edu.br)

## 📚 Disciplina

**XDES17 – Introdução à Codificação de Jogos Digitais**  
Instituto de Matemática e Computação  
Universidade Federal de Itajubá (UNIFEI)

---

*Desenvolvido em 2025*

