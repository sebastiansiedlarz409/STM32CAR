/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * @attention
  *
  * <h2><center>&copy; Copyright (c) 2020 STMicroelectronics.
  * All rights reserved.</center></h2>
  *
  * This software component is licensed by ST under BSD 3-Clause license,
  * the "License"; You may not use this file except in compliance with the
  * License. You may obtain a copy of the License at:
  *                        opensource.org/licenses/BSD-3-Clause
  *
  ******************************************************************************
  */
/* USER CODE END Header */
/* Includes ------------------------------------------------------------------*/
#include "main.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
TIM_HandleTypeDef htim4;

UART_HandleTypeDef huart1;
UART_HandleTypeDef huart3;

/* USER CODE BEGIN PV */
uint32_t PWM1 = 0;				//current pwm on channel 1
uint32_t PWM2 = 0;				//current pwm on channel 2

uint8_t dataUART1[32];			//buffor for uart data
/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_USART3_UART_Init(void);
static void MX_USART1_UART_Init(void);
static void MX_TIM4_Init(void);
/* USER CODE BEGIN PFP */

/*
 * printf override
 * */
int __io_putchar(int ch)
{
	HAL_UART_Transmit(&huart3, (uint8_t*)&ch, 1, 1000);
	return ch;
}

/*
 * Set pwm on specified channel
 * */
void SetPWM(uint8_t channelIndex, uint32_t value)
{
	TIM_OC_InitTypeDef sConfigOC = {0};

	sConfigOC.OCMode = TIM_OCMODE_PWM1;
	sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
	sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;

	if(channelIndex == 1){
		HAL_TIM_PWM_Stop(&htim4, TIM_CHANNEL_1);

		PWM1 = value;
		sConfigOC.Pulse = value;

		if (HAL_TIM_PWM_ConfigChannel(&htim4, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
		{
			Error_Handler();
		}

		HAL_TIM_PWM_Start(&htim4, TIM_CHANNEL_1);
	}

	if(channelIndex == 2){
		HAL_TIM_PWM_Stop(&htim4, TIM_CHANNEL_2);

		PWM2 = value;
		sConfigOC.Pulse = value;

		if (HAL_TIM_PWM_ConfigChannel(&htim4, &sConfigOC, TIM_CHANNEL_2) != HAL_OK)
		{
			Error_Handler();
		}

		HAL_TIM_PWM_Start(&htim4, TIM_CHANNEL_2);
	}
}

/*
 * Help to print response from HM10 with different data size
 * */
void PrintResponse(uint8_t size){
	for(uint8_t i = 0; i < size ;i++){
		printf("%c", dataUART1[i]);
	}
	printf("\r\n");
}

/*
 * Just send welcome message to HM10
 * */
void SendOK(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT", strlen("AT"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 2, 1000);

	printf("OK response: ");
	PrintResponse(2);

	HAL_Delay(100);
}

/*
 * Send bound rate to HM10
 * */
void SendBaud(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+BAUD0", strlen("AT+BAUD0"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 8, 1000);

	printf("Baud response: ");
	PrintResponse(8);

	HAL_Delay(100);
}

/*
 * Send BT name to HM10
 * */
void SendName(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+NAMEstm32car", strlen("AT+NAMEstm32car"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 16, 1000);

	printf("Name response: ");
	PrintResponse(16);

	HAL_Delay(100);
}

/*
 * Send PIN code to HM10
 * */
void SendPIN(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+PASS123456", strlen("AT+PASS123456"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 14, 1000);

	printf("Pin response: ");
	PrintResponse(13);

	HAL_Delay(100);
}

/*
 * Make PIN required (1) or not (0)
 * */
void SendBond(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+TYPE1", strlen("AT+TYPE1"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 8, 1000);

	printf("Bond response: ");
	PrintResponse(8);

	HAL_Delay(100);
}

/*
 * Set role master (0) or slave (1)
 * */
void SendRole(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+ROLE0", strlen("AT+ROLE0"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 8, 1000);

	printf("Role response: ");
	PrintResponse(8);

	HAL_Delay(100);
}

/*
 * Send power value to HM10
 * */
void SendPower(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+POWE2", strlen("AT+POWE2"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 8, 1000);

	printf("Power response: ");
	PrintResponse(8);

	HAL_Delay(100);
}

/*
 * Send mode to HM10
 * */
void SendMode(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+MODE2", strlen("AT+MODE2"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 8, 1000);

	printf("Mode response: ");
	PrintResponse(8);

	HAL_Delay(100);
}

/*
 * Send IMME type to HM10
 * */
void SendImme(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+IMME0", strlen("AT+IMME0"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 8, 1000);

	printf("Imme response: ");
	PrintResponse(8);

	HAL_Delay(100);
}

/*
 * Clear module config
 * */
void SendRenew(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+RENEW", strlen("AT+RENEW"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 8, 1000);

	printf("Renew response: ");
	PrintResponse(8);

	HAL_Delay(100);
}

/*
 * Clear last connected device value in module
 * */
void SendClear(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+CLEAR", strlen("AT+CLEAR"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 8, 1000);

	printf("Clear response: ");
	PrintResponse(8);

	HAL_Delay(100);
}

/*
 * Return firmware version
 * */
void SendVers(void)
{
	HAL_UART_Transmit(&huart1, (uint8_t*)"AT+VERS?", strlen("AT+VERS?"), 100);

	HAL_UART_Receive(&huart1, dataUART1, 12, 1000);

	printf("Verr response: ");
	PrintResponse(12);

	HAL_Delay(100);
}

/*
 * Calculate flecher checksum
 * */
uint16_t CalculateChecksum(void){

	uint16_t a = 0;
	uint16_t b = 0;

	for(uint8_t i = 0; i < 9 ; i++){
		a = (uint8_t)((a + dataUART1[i]) % 255);
		b = (uint8_t)((a + b) % 255);
	}

	return (uint16_t)((b << 8) | a);

}

void CaseAccelerometer(void){
	//throttle
	if(dataUART1[6] < 50){
		SetPWM(1, 150*dataUART1[6]);
	}
	else{
		SetPWM(1, 100*dataUART1[6]);
	}
}

void CaseButtons(void){
	//throttle
	SetPWM(1, 100*dataUART1[6]);
}

/*
 * Decide what to do with received data
 * */
void AnalyzeData(void)
{
	uint16_t received_checksum = (dataUART1[7] << 8) | dataUART1[8];

	dataUART1[7] = 0;
	dataUART1[8] = 0;

	uint16_t calculated_checksum = CalculateChecksum();

	if(calculated_checksum != received_checksum){
		printf("Mode: E, Values: ERROR %X %X\r\n", received_checksum, calculated_checksum);
	}
	else{
		printf("Mode: %c, Values: %c%u %c%u %c%u %X %X\r\n", (char)dataUART1[0],
				(char)dataUART1[1], dataUART1[2], (char)dataUART1[3],
				dataUART1[4], (char)dataUART1[5], dataUART1[6], received_checksum, calculated_checksum);

		if(dataUART1[0] == 'A'){
			CaseAccelerometer();
		}

		if(dataUART1[0] == 'B'){
			CaseButtons();
		}
	}
}

/*
 * Set data to 0 when transmission drop
 * */
void ResetData(){
	dataUART1[0] = 0;
	dataUART1[1] = 0;
	dataUART1[2] = 0;
	dataUART1[3] = 0;
	dataUART1[4] = 0;
	dataUART1[5] = 0;
	dataUART1[6] = 0;
	dataUART1[7] = 0;
	dataUART1[8] = 0;
	dataUART1[9] = 0;
}

/*
 * UART INT callback
 * */
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
	AnalyzeData();

	HAL_NVIC_ClearPendingIRQ(USART1_IRQn);
	ResetData();
	HAL_UART_Receive_IT(&huart1, dataUART1, 9);
}

/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */

/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{
  /* USER CODE BEGIN 1 */

  /* USER CODE END 1 */

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_USART3_UART_Init();
  MX_USART1_UART_Init();
  MX_TIM4_Init();
  /* USER CODE BEGIN 2 */

  printf("STARTED\r\n");

  //send config to HM10
  SendOK();
  SendVers();
  //SendRenew();
  SendClear();
  SendBaud();
  SendName();
  SendBond();
  SendPIN();
  SendRole();
  SendPower();
  SendMode();
  SendImme();

  //enable uart interrupt
  HAL_UART_Receive_IT(&huart1, dataUART1, 9);

  //SetPWM(1, 1000);
  printf("Config sent\r\n");

  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
	HAL_Delay(600);
	HAL_UART_Receive_IT(&huart1, dataUART1, 9);

    /* USER CODE END WHILE */

    /* USER CODE BEGIN 3 */
  }
  /* USER CODE END 3 */
}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

  /** Initializes the RCC Oscillators according to the specified parameters
  * in the RCC_OscInitTypeDef structure.
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSI;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.HSICalibrationValue = RCC_HSICALIBRATION_DEFAULT;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSI_DIV2;
  RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL16;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }
  /** Initializes the CPU, AHB and APB buses clocks
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief TIM4 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM4_Init(void)
{

  /* USER CODE BEGIN TIM4_Init 0 */

  /* USER CODE END TIM4_Init 0 */

  TIM_MasterConfigTypeDef sMasterConfig = {0};
  TIM_OC_InitTypeDef sConfigOC = {0};

  /* USER CODE BEGIN TIM4_Init 1 */

  /* USER CODE END TIM4_Init 1 */
  htim4.Instance = TIM4;
  htim4.Init.Prescaler = 800-1;
  htim4.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim4.Init.Period = 1000-1;
  htim4.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim4.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_PWM_Init(&htim4) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim4, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigOC.OCMode = TIM_OCMODE_PWM1;
  sConfigOC.Pulse = 0;
  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
  if (HAL_TIM_PWM_ConfigChannel(&htim4, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_ConfigChannel(&htim4, &sConfigOC, TIM_CHANNEL_2) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM4_Init 2 */

  /* USER CODE END TIM4_Init 2 */
  HAL_TIM_MspPostInit(&htim4);

}

/**
  * @brief USART1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART1_UART_Init(void)
{

  /* USER CODE BEGIN USART1_Init 0 */

  /* USER CODE END USART1_Init 0 */

  /* USER CODE BEGIN USART1_Init 1 */

  /* USER CODE END USART1_Init 1 */
  huart1.Instance = USART1;
  huart1.Init.BaudRate = 9600;
  huart1.Init.WordLength = UART_WORDLENGTH_8B;
  huart1.Init.StopBits = UART_STOPBITS_1;
  huart1.Init.Parity = UART_PARITY_NONE;
  huart1.Init.Mode = UART_MODE_TX_RX;
  huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart1.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART1_Init 2 */

  /* USER CODE END USART1_Init 2 */

}

/**
  * @brief USART3 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART3_UART_Init(void)
{

  /* USER CODE BEGIN USART3_Init 0 */

  /* USER CODE END USART3_Init 0 */

  /* USER CODE BEGIN USART3_Init 1 */

  /* USER CODE END USART3_Init 1 */
  huart3.Instance = USART3;
  huart3.Init.BaudRate = 9600;
  huart3.Init.WordLength = UART_WORDLENGTH_8B;
  huart3.Init.StopBits = UART_STOPBITS_1;
  huart3.Init.Parity = UART_PARITY_NONE;
  huart3.Init.Mode = UART_MODE_TX_RX;
  huart3.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart3.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart3) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART3_Init 2 */

  /* USER CODE END USART3_Init 2 */

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOC_CLK_ENABLE();
  __HAL_RCC_GPIOD_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(LD2_GPIO_Port, LD2_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin : B1_Pin */
  GPIO_InitStruct.Pin = B1_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_RISING;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(B1_GPIO_Port, &GPIO_InitStruct);

  /*Configure GPIO pins : USART_TX_Pin USART_RX_Pin */
  GPIO_InitStruct.Pin = USART_TX_Pin|USART_RX_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_AF_PP;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

  /*Configure GPIO pin : LD2_Pin */
  GPIO_InitStruct.Pin = LD2_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(LD2_GPIO_Port, &GPIO_InitStruct);

  /* EXTI interrupt init*/
  HAL_NVIC_SetPriority(EXTI15_10_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI15_10_IRQn);

}

/* USER CODE BEGIN 4 */

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */

  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     tex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
