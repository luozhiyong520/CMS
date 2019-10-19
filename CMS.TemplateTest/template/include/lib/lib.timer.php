<?php 

class lib_timer
{

		public $starttime = NULL;
		public $stoptime = NULL;
		public $spendtime = NULL;

		public function getmicrotime( )
		{
				list( $usec, $sec ) = explode( " ", microtime( ) );
				return ( double )$usec + ( double )$sec;
		}

		public function start( )
		{
				$this->starttime = $this->getmicrotime( );
		}

		public function display( )
		{
				$this->stoptime = $this->getmicrotime( );
				$this->spendtime = $this->stoptime - $this->starttime;
				return round( $this->spendtime, 4 );
		}

}

?>
