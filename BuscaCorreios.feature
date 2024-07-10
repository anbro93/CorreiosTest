Feature: Busca de CEP/Rastreio nos Correios

  Scenario: Buscar por um CEP invalido seguido de um CEP valido
    Given que eu estou na pagina de busca de CEP dos Correios
    When eu busco pelo CEP "80700-000"
    Then o resultado deve indicar que o CEP e invalido
    When eu busco pelo CEP "01013-001"
    Then o resultado deve conter o endereco "Rua Quinze de Novembro - lado ímpar"

  Scenario: Buscar um Rastreio inválido
    Given que eu estou na pagina de busca de Rastreio dos Correios
    When eu busco pelo rastreio "SS987654321BR"
    Then o resultado deve indicar que o rastreio e invalido
